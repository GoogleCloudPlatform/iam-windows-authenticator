﻿//
// Copyright 2022 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Google.Solutions.WWAuth.Interop
{
    internal class NativeMethods
    {
        public const int ERROR_DIRECTORY = 267;

        //---------------------------------------------------------------------
        // Console I/O.
        //---------------------------------------------------------------------

        internal const int ATTACH_PARENT_PROCESS = -1;

        internal enum StandardHandle : uint
        {
            Input = unchecked((uint)-10),
            Output = unchecked((uint)-11),
            Error = unchecked((uint)-12)
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(StandardHandle nStdHandle);


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetStdHandle(StandardHandle nStdHandle, IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern FileType GetFileType(IntPtr handle);

        internal enum FileType : uint
        {
            Unknown = 0x0000,
            Disk = 0x0001,
            Char = 0x0002,
            Pipe = 0x0003
        }

        //---------------------------------------------------------------------
        // TaskDialog.
        //---------------------------------------------------------------------

        public static readonly IntPtr TD_WARNING_ICON = new IntPtr(65535);
        public static readonly IntPtr TD_ERROR_ICON = new IntPtr(65534);
        public static readonly IntPtr TD_INFORMATION_ICON = new IntPtr(65533);
        public static readonly IntPtr TD_SHIELD_ICON = new IntPtr(65532);
        public static readonly IntPtr TD_SHIELD_ICON_INFO_BACKGROUND = new IntPtr(65531);
        public static readonly IntPtr TD_SHIELD_ICON_WARNING_BACKGROUND = new IntPtr(65530);

        [Flags]
        internal enum TASKDIALOG_FLAGS : uint
        {
            TDF_ENABLE_HYPERLINKS = 0x0001,
            TDF_USE_HICON_MAIN = 0x0002,
            TDF_USE_HICON_FOOTER = 0x0004,
            TDF_ALLOW_DIALOG_CANCELLATION = 0x0008,
            TDF_USE_COMMAND_LINKS = 0x0010,
            TDF_USE_COMMAND_LINKS_NO_ICON = 0x0020,
            TDF_EXPAND_FOOTER_AREA = 0x0040,
            TDF_EXPANDED_BY_DEFAULT = 0x0080,
            TDF_VERIFICATION_FLAG_CHECKED = 0x0100,
            TDF_SHOW_PROGRESS_BAR = 0x0200,
            TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x0400,
            TDF_CALLBACK_TIMER = 0x0800,
            TDF_POSITION_RELATIVE_TO_WINDOW = 0x1000,
            TDF_RTL_LAYOUT = 0x2000,
            TDF_NO_DEFAULT_RADIO_BUTTON = 0x4000,
            TDF_CAN_BE_MINIMIZED = 0x8000
        }

        [Flags]
        public enum TASKDIALOG_COMMON_BUTTON_FLAGS : uint
        {
            TDCBF_OK_BUTTON = 0x0001,
            TDCBF_YES_BUTTON = 0x0002,
            TDCBF_NO_BUTTON = 0x0004,
            TDCBF_CANCEL_BUTTON = 0x0008,
            TDCBF_RETRY_BUTTON = 0x0010,
            TDCBF_CLOSE_BUTTON = 0x0020,
        }

        public enum TASKDIALOG_NOTIFICATIONS : uint
        {
            TDN_CREATED = 0,
            TDN_NAVIGATED = 1,
            TDN_BUTTON_CLICKED = 2,
            TDN_HYPERLINK_CLICKED = 3,
            TDN_TIMER = 4,
            TDN_DESTROYED = 5,
            TDN_RADIO_BUTTON_CLICKED = 6,
            TDN_DIALOG_CONSTRUCTED = 7,
            TDN_VERIFICATION_CLICKED = 8,
            TDN_HELP = 9,
            TDN_EXPANDO_BUTTON_CLICKED = 10
        }

        public const int IDOK = 1;
        public const int IDCANCEL = 2;
        public const int IDABORT = 3;
        public const int IDRETRY = 4;
        public const int IDIGNORE = 5;
        public const int IDYES = 6;
        public const int IDNO = 7;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        internal struct TASKDIALOGCONFIG
        {
            public uint cbSize;
            public IntPtr hwndParent;
            public IntPtr hInstance;
            public TASKDIALOG_FLAGS dwFlags;
            public TASKDIALOG_COMMON_BUTTON_FLAGS dwCommonButtons;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszWindowTitle;

            public IntPtr MainIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMainInstruction;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszContent;

            public uint cButtons;

            public IntPtr pButtons;

            public int nDefaultButton;
            public uint cRadioButtons;
            public IntPtr pRadioButtons;
            public int nDefaultRadioButton;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszVerificationText;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedInformation;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedControlText;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCollapsedControlText;

            public IntPtr FooterIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;

            public TaskDialogCallback pfCallback;
            public IntPtr lpCallbackData;
            public uint cxWidth;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        internal struct TASKDIALOG_BUTTON_RAW
        {
            public int nButtonID;

            public IntPtr pszButtonText;
        }

        internal delegate int TaskDialogCallback(
            [In] IntPtr hwnd,
            [In] TASKDIALOG_NOTIFICATIONS msg,
            [In] UIntPtr wParam,
            [In] IntPtr lParam,
            [In] IntPtr refData);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("ComCtl32", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void TaskDialogIndirect(
            [In] ref TASKDIALOGCONFIG pTaskConfig,
            [Out] out int pnButton,
            [Out] out int pnRadioButton,
            [Out] out bool pfVerificationFlagChecked);


        //---------------------------------------------------------------------
        // CredUI.
        //---------------------------------------------------------------------

        public const int ERROR_NOERROR = 0;
        public const int ERROR_CANCELLED = 1223;
        public const int ERROR_NO_SUCH_LOGON_SESSION = 1312;
        public const int ERROR_NOT_FOUND = 1168;
        public const int ERROR_INVALID_ACCOUNT_NAME = 1315;
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        public const int ERROR_INVALID_PARAMETER = 87;
        public const int ERROR_INVALID_FLAGS = 1004;
        public const int ERROR_BAD_ARGUMENTS = 160;

        [Flags]
        public enum CREDUI_FLAGS
        {
            ALWAYS_SHOW_UI = 0x00080,
            COMPLETE_USERNAME = 0x00800,
            DO_NOT_PERSIST = 0x00002,
            EXCLUDE_CERTIFICATES = 0x00008,
            EXPECT_CONFIRMATION = 0x20000,
            GENERIC_CREDENTIALS = 0x40000,
            INCORRECT_PASSWORD = 0x00001,
            KEEP_USERNAME = 0x100000,
            PASSWORD_ONLY_OK = 0x00200,
            PERSIST = 0x01000,
            REQUEST_ADMINISTRATOR = 0x00004,
            REQUIRE_CERTIFICATE = 0x00010,
            REQUIRE_SMARTCARD = 0x00100,
            SERVER_CREDENTIAL = 0X04000,
            SHOW_SAVE_CHECK_BOX = 0x00040,
            USERNAME_TARGET_CREDENTIALS = 0x80000,
            VALIDATE_USERNAME = 0x00400
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CREDUI_INFO
        {
            public int cbSize;
            public IntPtr hwndParent;
            public string pszMessageText;
            public string pszCaptionText;
            public IntPtr hbmBanner;
        }

        [DllImport("credui", CharSet = CharSet.Unicode)]
        public static extern int CredUIPromptForCredentialsW(
            ref CREDUI_INFO uiInfo,
            string targetName,
            IntPtr reserved1,
            int iError,
            StringBuilder userName,
            int maxUserName,
            StringBuilder password,
            int maxPassword,
            [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
            CREDUI_FLAGS flags);

        //---------------------------------------------------------------------
        // Paths.
        //---------------------------------------------------------------------

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetShortPathName(
            string pathName,
            StringBuilder shortName,
            int cbShortName);
    }
}
