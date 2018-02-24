using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CP.Window.Forms
{
	/// <summary>
	/// Direction in which to find
	/// </summary>
	public enum FindDirection
	{
		/// <summary>
		/// Finds upwards in the document
		/// </summary>
		Up = 0,
		/// <summary>
		/// Finds downwards in the document
		/// </summary>
		Down = 1,
	}
	
	/// <summary>
	/// Enumeration for dialog type
	/// </summary>
	public enum FindReplaceDialogType
	{
		/// <summary>
		/// Find dialog
		/// </summary>
		Find = 0,
		/// <summary>
		/// Replace dialog
		/// </summary>
		Replace = 1
	}
	
	/// <summary>
	/// FindReplace dialog component for .NET
	/// </summary>
	public class FindReplaceDialog : System.ComponentModel.Component, IMessageFilter
	{
		#region Native window that subclasses the owner window
		private class OwnerWindow : NativeWindow
		{
			FindReplaceDialog source;

			internal OwnerWindow(FindReplaceDialog source)
			{
				this.source = source;
			}
			
			protected override void WndProc(ref System.Windows.Forms.Message m)
			{
				if (m.Msg == FindReplaceDialog.findmsgstring)
				{
					source.HandleFindMsgString(m);
				}
				else
					DefWndProc(ref m);
			}
		}
		#endregion
		
		#region Data members
		
		/// <summary>
		/// Dialog flags
		/// </summary>
		private int flags;
		/// <summary>
		/// Copy of memeory passed to FindReplace
		/// </summary>
		private IntPtr fr;
		/// <summary>
		/// Dialog type
		/// </summary>
		private FindReplaceDialogType dialogType;
		/// <summary>
		/// Handle of the dialog
		/// </summary>
		private IntPtr hwndDlg;
		/// <summary>
		/// Window that subclasses the owner
		/// </summary>
		private OwnerWindow owner;
		/// <summary>
		/// Registered message used by FindReplace common dialog
		/// </summary>
		internal static int findmsgstring = 0;
		/// <summary>
		/// String that needs to be found
		/// </summary>
		private string findwhat;
		/// <summary>
		/// Strings that it needs to be replaced with
		/// </summary>
		private string replacewith;
		/// <summary>
		/// Buffer to whole find what string
		/// </summary>
		private IntPtr findwhatbuffer = IntPtr.Zero;
		/// <summary>
		/// Buffer to store replace with
		/// </summary>
		private IntPtr replacewithbuffer = IntPtr.Zero;

		#endregion

		/// <summary>
		/// Initializes a new instance of FindReplaceDialog
		/// </summary>
		public FindReplaceDialog()
		{
			owner = new OwnerWindow(this);
		}
		
		#region Code related to unamaged API
		
		[StructLayout(LayoutKind.Sequential)]
		internal class FindReplace
		{
			internal int cbSize;
			internal IntPtr hwndOwner;
			internal IntPtr hInstance;
			internal int flags;
			internal IntPtr findwhat;
			internal IntPtr replacewith;
			internal short findwhatlen;
			internal short replacewithlen;
			internal IntPtr custdata;
			internal IntPtr hookproc;
			internal IntPtr templateName;
		}

		[DllImport("Comdlg32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		static extern IntPtr FindText(IntPtr lpfr);

		[DllImport("Comdlg32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		static extern IntPtr ReplaceText(IntPtr lpfr);

		[DllImport("User32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		static extern int RegisterWindowMessage([MarshalAs(UnmanagedType.LPTStr)] string message);
		
		[DllImport("User32.dll")]
		static extern int DestroyWindow(IntPtr hwnd);
		
		[StructLayout(LayoutKind.Sequential)]
		struct MSG
		{
			internal IntPtr hwnd;
			internal int message;
			internal IntPtr wparam;
			internal IntPtr lparam;
			int time;
			long x;
			long y;
		}

		[DllImport("User32.dll")]
		static extern bool IsDialogMessage(IntPtr hwnd, ref MSG msg);
		#endregion

		#region Constaints
		
		const int FR_DOWN                         = 0x00000001;
		const int FR_MATCHWHOLEWORD               = 0x00000002;
		const int FR_MATCHCASE                    = 0x00000004;
		const int FR_FINDNEXT                     = 0x00000008;
		const int FR_REPLACE                      = 0x00000010;
		const int FR_REPLACEALL                   = 0x00000020;
		const int FR_DIALOGTERM                   = 0x00000040;
		const int FR_SHOWHELP                     = 0x00000080;
		const int FR_ENABLEHOOK                   = 0x00000100;
		const int FR_ENABLETEMPLATE               = 0x00000200;
		const int FR_DISABLEUPDOWN                = 0x00000400;
		const int FR_DISABLEMATCHCASE             = 0x00000800;
		const int FR_DISABLEWHOLEWORD             = 0x00001000;
		const int FR_ENABLETEMPLATEHANDLE         = 0x00002000;
		const int FR_HIDEUPDOWN                   = 0x00004000;
		const int FR_HIDEMATCHCASE                = 0x00008000;
		const int FR_HIDEWHOLEWORD                = 0x00010000;

		#endregion
		
		#region Helper Functions
		private void SetOption(int option, bool b)
		{
			if (b)
			{
				flags |= option;
			}
			else
			{
				flags &= ~option;
			}
		}
		
		private bool GetOption(int option)
		{
			return (flags & option) != 0;
		}
		
		private IntPtr StringToBuffer(string str, ref short length)
		{
			IntPtr ret = Marshal.StringToHGlobalAuto(str);
			
			length = (short)str.Length;

			if (str == null || str.Length < MINBUFFERSIZE)
			{
				ret = Marshal.ReAllocHGlobal(ret, new IntPtr(MINBUFFERSIZE * Marshal.SystemDefaultCharSize));
				length = (short)MINBUFFERSIZE;
			}
					
			return ret;
		}

		#endregion
		
		#region Properties that get and set various options
		/// <summary>
		/// Gets or sets the search direction Up or down
		/// </summary>
		public FindDirection Direction
		{
			get
			{
				return GetOption(FR_DOWN) ?
					FindDirection.Down : FindDirection.Up;
			}
			set
			{
				SetOption(FR_DOWN, value == FindDirection.Down);
			}
		}
		
		/// <summary>
		/// Hides up down radio buttons
		/// </summary>
		public bool HideUpDown
		{
			get
			{
				return GetOption(FR_HIDEUPDOWN);
			}
			set
			{
				SetOption(FR_HIDEUPDOWN, value);
			}
        }
		
		/// <summary>
		/// Hides match case check box
		/// </summary>
		public bool HideMatchCase
		{
			get
			{
				return GetOption(FR_HIDEMATCHCASE);
			}
			set
			{
				SetOption(FR_HIDEMATCHCASE, value);
			}
		}	
		
		/// <summary>
		/// Hides whole word check box
		/// </summary>
		public bool HideWholeWord
		{
			get
			{
				return GetOption(FR_HIDEWHOLEWORD);
			}
			set
			{
				SetOption(FR_HIDEWHOLEWORD, value);
			}
		}

		/// <summary>
		/// Checks match case option while searching
		/// </summary>
		public bool MatchCase
		{
			get
			{
				return GetOption(FR_MATCHCASE);
			}
			set
			{
				SetOption(FR_MATCHCASE, value);
			}
		}

		/// <summary>
		/// Disables match case check box
		/// </summary>
		public bool DisableMatchCase
		{
			get
			{
				return GetOption(FR_DISABLEMATCHCASE);
			}
			set
			{
				SetOption(FR_DISABLEMATCHCASE, value);
			}
		}
		
		/// <summary>
		/// Disables up/down radio buttons
		/// </summary>
		public bool DisableUpDown
		{
			get
			{
				return GetOption(FR_DISABLEUPDOWN);
			}
			set
			{
				SetOption(FR_DISABLEUPDOWN, value);
			}
		}

		/// <summary>
		/// Disables whole word check box
		/// </summary>
		public bool DisableWholeWord
		{
			get
			{
				return GetOption(FR_DISABLEWHOLEWORD);
			}
			set
			{
				SetOption(FR_DISABLEWHOLEWORD, value);
			}
		}

		/// <summary>
		/// Matches whole word
		/// </summary>
		public bool MatchWholeWord
		{
			get
			{
				return GetOption(FR_MATCHWHOLEWORD);
			}
			set
			{
				SetOption(FR_MATCHWHOLEWORD, value);
			}
		}
		
		/// <summary>
		/// Shows the help button in the title bar
		/// </summary>
		public bool ShowHelp
		{
			get
			{
				return GetOption(FR_SHOWHELP);
			}
			set
			{
				SetOption(FR_SHOWHELP, value);
			}
		}

		#endregion
		
		#region Other properties
		/// <summary>
		/// Gets or sets the string which needs to be found
		/// </summary>
		public string FindWhat
		{
			get
			{
				return findwhat;
			}
			set
			{
				findwhat = value;
			}
		}

		/// <summary>
		/// Gets or sets the string to replaced with
		/// </summary>
		public string ReplaceWith
		{
			get
			{
				return replacewith;
			}
			set
			{
				replacewith = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the type of dialog
		/// </summary>
		public FindReplaceDialogType Type
		{
			get
			{
				return dialogType;
			}
			set
			{
				dialogType = value;
			}
		}
		
		const int MINBUFFERSIZE = 256;

		#endregion

		#region Events and firing code
		/// <summary>
		/// Fired when user clicks the replace button
		/// </summary>
		public event EventHandler Replace;
		/// <summary>
		/// Fired when the user clicks ReplaceAll button
		/// </summary>
		public event EventHandler ReplaceAll;
		/// <summary>
		/// Fired when the user cliecks FindNext button
		/// </summary>
		public event EventHandler FindNext;
		/// <summary>
		/// Fired when the dialog is terminated by the user
		/// </summary>
		public event EventHandler DialogTerminate;
		
		/// <summary>
		/// Allows derived classes to handle replace event
		/// </summary>
		protected virtual void OnReplace()
		{
			if (Replace != null)
				Replace(this, EventArgs.Empty);
		}

		/// <summary>
		/// Allows derived classes to handle replace all event
		/// </summary>
		protected virtual void OnReplaceAll()
		{
			if (ReplaceAll != null)
				ReplaceAll(this, EventArgs.Empty);
		}

		/// <summary>
		/// Allows derive classes to handle find next event
		/// </summary>
		protected virtual void OnFindNext()
		{
			if (FindNext != null)
				FindNext(this, EventArgs.Empty);
		}

		/// <summary>
		/// Allows derive classes to dialog terminate event
		/// </summary>
		protected virtual void OnDialogTerminate()
		{
			hwndDlg = IntPtr.Zero;
			this.owner.ReleaseHandle();
			Application.RemoveMessageFilter(this);

			if (DialogTerminate != null)
				DialogTerminate(this, EventArgs.Empty);
		}

		internal void HandleFindMsgString(Message m)
		{
			FindReplace tempfr = (FindReplace)Marshal.PtrToStructure(m.LParam, typeof(FindReplace));
			
			flags = tempfr.flags;
			findwhat = Marshal.PtrToStringAuto(tempfr.findwhat);
			replacewith = Marshal.PtrToStringAuto(tempfr.replacewith);
								
			if (GetOption(FR_DIALOGTERM))
			{
				SetOption(FR_DIALOGTERM, false);	
				OnDialogTerminate();
			}
			else if (GetOption(FR_FINDNEXT))
			{
				SetOption(FR_FINDNEXT, false);	
				OnFindNext();
			}
			else if (GetOption(FR_REPLACE))
			{
				SetOption(FR_REPLACE, false);	
				OnReplace();
			}
			else if (GetOption(FR_REPLACEALL))
			{
				SetOption(FR_REPLACEALL, false);
				OnReplaceAll();
			}
		}

		#endregion
		
		/// <summary>
		/// Shows the dialog. Throws an execption if something goes wrong
		/// </summary>
		/// <param name="owner">The owner of the dialog box (a form)</param>
		public void ShowDialog(IWin32Window owner)
		{
			if (findmsgstring == 0)
				findmsgstring = RegisterWindowMessage("commdlg_FindReplace"); 
			
			//Subclass the owner
			this.owner.AssignHandle(owner.Handle);

			CleanUp();

			//Create a new FindReplace
			FindReplace tempfr = new FindReplace();

			tempfr.hwndOwner = owner.Handle;
			tempfr.cbSize = Marshal.SizeOf(typeof(FindReplace));
			findwhatbuffer = StringToBuffer(findwhat, ref tempfr.findwhatlen);
			tempfr.findwhat = findwhatbuffer;
			replacewithbuffer = StringToBuffer(replacewith, ref tempfr.replacewithlen);
			tempfr.replacewith = replacewithbuffer;
			tempfr.flags = flags;
			tempfr.hInstance = IntPtr.Zero;
			
			fr = Marshal.AllocHGlobal(Marshal.SizeOf(tempfr));
			Marshal.StructureToPtr(tempfr, fr, true);
			
			Application.AddMessageFilter(this);

			if (dialogType == FindReplaceDialogType.Find)
				hwndDlg = FindText(fr);
			else
				hwndDlg = ReplaceText(fr);
			
			if (hwndDlg == IntPtr.Zero)
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}

		#region Cleanup and dispose
		
		private void CleanUp()
		{
			if (findwhatbuffer != IntPtr.Zero)
				Marshal.FreeHGlobal(findwhatbuffer);
			
			if (replacewithbuffer != IntPtr.Zero)
				Marshal.FreeHGlobal(replacewithbuffer);
			
			if (fr != IntPtr.Zero)
			{
				FindReplace tempfr = (FindReplace)Marshal.PtrToStructure(fr, typeof(FindReplace));
				Marshal.FreeHGlobal(fr);
			}

			fr = IntPtr.Zero;
		}
		
		/// <summary>
		/// Destroys the dialog if already open
		/// </summary>
		/// <param name="disposing">Free unmanaged resources</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			
			CleanUp();

			if (hwndDlg != IntPtr.Zero)
			{
				DestroyWindow(hwndDlg);
				Application.RemoveMessageFilter(this);
			}
		}
		#endregion

		#region Implementation of IMessageFilter
		/// <summary>
		/// Filters messages by calling IsDialogMessage so that dialog keys like TAB, ESC etc. work
		/// </summary>
		/// <param name="m">The message that arrived in the message loop</param>
		/// <returns></returns>
		public bool PreFilterMessage(ref System.Windows.Forms.Message m)
		{
			MSG msg = new MSG();
			msg.hwnd = m.HWnd;
			msg.lparam = m.LParam;
			msg.message = m.Msg;
			msg.wparam = m.WParam;
			
			return IsDialogMessage(hwndDlg, ref msg);

			//return false;
		}

        internal void ShowDialog()
        {
          
        }
        #endregion
    }
}
