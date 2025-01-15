using System;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E1 RID: 225
	internal static class Defaults
	{
		// Token: 0x040007C4 RID: 1988
		public const CompatibilityMode DefaultServerMode = CompatibilityMode.PowerBI;

		// Token: 0x040007C5 RID: 1989
		public const AnnotationVisibility Annotation_Visibility = AnnotationVisibility.None;

		// Token: 0x040007C6 RID: 1990
		public const PermissionSet ClrAssembly_PermissionSet = PermissionSet.Safe;

		// Token: 0x040007C7 RID: 1991
		public const ClrAssemblyFileType ClrAssemblyFile_Type = ClrAssemblyFileType.Main;

		// Token: 0x040007C8 RID: 1992
		public const bool Database_Visible = true;

		// Token: 0x040007C9 RID: 1993
		public const int Database_ProcessingPriority = 0;

		// Token: 0x040007CA RID: 1994
		public const ReadWriteMode Database_ReadWriteMode = ReadWriteMode.ReadWrite;

		// Token: 0x040007CB RID: 1995
		public const StorageEngineUsed Database_StorageEngineUsed = StorageEngineUsed.Traditional;

		// Token: 0x040007CC RID: 1996
		public const int Database_CompatibilityLevel = 1050;

		// Token: 0x040007CD RID: 1997
		public const DirectQueryMode Database_DirectQueryMode = DirectQueryMode.InMemory;

		// Token: 0x040007CE RID: 1998
		public const bool DatabasePermission_Administer = false;

		// Token: 0x040007CF RID: 1999
		public const long ErrorConfiguration_KeyErrorLimit = 0L;

		// Token: 0x040007D0 RID: 2000
		public const KeyErrorAction ErrorConfiguration_KeyErrorAction = KeyErrorAction.ConvertToUnknown;

		// Token: 0x040007D1 RID: 2001
		public const KeyErrorLimitAction ErrorConfiguration_KeyErrorLimitAction = KeyErrorLimitAction.StopProcessing;

		// Token: 0x040007D2 RID: 2002
		public const ErrorOption ErrorConfiguration_KeyNotFound = ErrorOption.ReportAndContinue;

		// Token: 0x040007D3 RID: 2003
		public const ErrorOption ErrorConfiguration_KeyDuplicate = ErrorOption.IgnoreError;

		// Token: 0x040007D4 RID: 2004
		public const ErrorOption ErrorConfiguration_NullKeyConvertedToUnknown = ErrorOption.IgnoreError;

		// Token: 0x040007D5 RID: 2005
		public const ErrorOption ErrorConfiguration_NullKeyNotAllowed = ErrorOption.ReportAndContinue;

		// Token: 0x040007D6 RID: 2006
		public const ErrorOption ErrorConfiguration_CalculationError = ErrorOption.IgnoreError;

		// Token: 0x040007D7 RID: 2007
		public const ImpersonationMode ImpersonationInfo_ImpersonationMode = ImpersonationMode.Default;

		// Token: 0x040007D8 RID: 2008
		public const ImpersonationInfoSecurity ImpersonationInfo_ImpersonationInfoSecurity = ImpersonationInfoSecurity.Unchanged;

		// Token: 0x040007D9 RID: 2009
		public const ServerEdition Server_Edition = ServerEdition.Standard;

		// Token: 0x040007DA RID: 2010
		public const long Server_EditionID = -1L;

		// Token: 0x040007DB RID: 2011
		public const ServerMode Server_ServerMode = ServerMode.Default;

		// Token: 0x040007DC RID: 2012
		public const ServerLocation Server_ServerLocation = ServerLocation.OnPremise;

		// Token: 0x040007DD RID: 2013
		public const CompatibilityMode Server_Unknown_CompatibilityMode = CompatibilityMode.Unknown;

		// Token: 0x040007DE RID: 2014
		public const int Server_Unknown_DefaultCompatibilityLevel = 0;

		// Token: 0x040007DF RID: 2015
		public const string Server_Unknown_SuppertedCompatibilityLevels = null;

		// Token: 0x040007E0 RID: 2016
		public const bool ServerProperty_RequiresRestart = false;

		// Token: 0x040007E1 RID: 2017
		public const bool ServerProperty_IsReadOnly = false;

		// Token: 0x040007E2 RID: 2018
		public const bool ServerProperty_DisplayFlag = true;

		// Token: 0x040007E3 RID: 2019
		public const ServerPropertyCategory ServerProperty_Category = ServerPropertyCategory.Basic;

		// Token: 0x040007E4 RID: 2020
		public const bool Trace_LogFileAppend = true;

		// Token: 0x040007E5 RID: 2021
		public const bool Trace_Audit = false;

		// Token: 0x040007E6 RID: 2022
		public const bool Trace_LogFileRollover = false;

		// Token: 0x040007E7 RID: 2023
		public const bool Trace_AutoRestart = false;
	}
}
