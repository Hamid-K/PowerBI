using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000411 RID: 1041
	internal class SharePointUserSettings
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x00064BE0 File Offset: 0x00062DE0
		private SharePointUserSettings()
		{
			this.ApiVersion = SharePointApiVersion.SP14;
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00064BEF File Offset: 0x00062DEF
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x00064BF7 File Offset: 0x00062DF7
		public SharePointApiVersion ApiVersion { get; private set; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x00064C00 File Offset: 0x00062E00
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x00064C08 File Offset: 0x00062E08
		public bool IsImplementationTwo { get; private set; }

		// Token: 0x060023B0 RID: 9136 RVA: 0x00064C11 File Offset: 0x00062E11
		public static SharePointUserSettings New(Value options, FileHelper.FolderOptions enumeration)
		{
			if (enumeration == FileHelper.FolderOptions.EnumerateTables)
			{
				return SharePointUserSettings.TablesSettings(options);
			}
			return SharePointUserSettings.FoldersAndFilesSettings(options);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00064C28 File Offset: 0x00062E28
		private static SharePointUserSettings TablesSettings(Value options)
		{
			SharePointUserSettings sharePointUserSettings = new SharePointUserSettings();
			if (!options.IsNull)
			{
				RecordValue asRecord = options.AsRecord;
				Options.ValidateOptions(asRecord, SharePointUserSettings.allValidOptionKeysTables, "SharePoint List", null);
				Value value;
				if (asRecord.TryGetValue("Implementation", out value))
				{
					if (value.IsNull)
					{
						sharePointUserSettings.IsImplementationTwo = false;
					}
					else
					{
						if (!(value.AsString == "2.0"))
						{
							throw ValueException.NewExpressionError<Message2>(Strings.UnsupportedSharePointListImplementation(value.AsString, Strings.ValidSharePointListImplementation), null, null);
						}
						sharePointUserSettings.IsImplementationTwo = true;
					}
				}
				if (!sharePointUserSettings.IsImplementationTwo)
				{
					Options.ValidateOptions(asRecord, SharePointUserSettings.allValidOptionKeysTablesImpl1, "SharePoint List 1.0", null);
					sharePointUserSettings.ApiVersion = SharePointUserSettings.GetApiVersion(asRecord);
				}
			}
			return sharePointUserSettings;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00064CE0 File Offset: 0x00062EE0
		private static SharePointUserSettings FoldersAndFilesSettings(Value options)
		{
			SharePointUserSettings sharePointUserSettings = new SharePointUserSettings();
			if (!options.IsNull)
			{
				RecordValue asRecord = options.AsRecord;
				Options.ValidateOptions(asRecord, SharePointUserSettings.validOptionKeysFolder, "SharePoint", null);
				sharePointUserSettings.ApiVersion = SharePointUserSettings.GetApiVersion(asRecord);
			}
			return sharePointUserSettings;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00064D24 File Offset: 0x00062F24
		private static SharePointApiVersion GetApiVersion(RecordValue optionsRecord)
		{
			Value value;
			if (optionsRecord.TryGetValue("ApiVersion", out value))
			{
				if (value.IsNumber)
				{
					int asInteger = value.AsInteger32;
					if (asInteger != 14 && asInteger != 15)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.UnsupportedSharePointVersionNumber(value), null, null);
					}
					if (asInteger == 15)
					{
						return SharePointApiVersion.SP15;
					}
				}
				else
				{
					if (value.IsText && value.AsString.Equals("Auto", StringComparison.OrdinalIgnoreCase))
					{
						return SharePointApiVersion.Auto;
					}
					throw ValueException.NewExpressionError<Message1>(Strings.UnsupportedSharePointVersion(value), null, null);
				}
			}
			return SharePointApiVersion.SP14;
		}

		// Token: 0x04000E4D RID: 3661
		private const string AutoKey = "Auto";

		// Token: 0x04000E4E RID: 3662
		private const SharePointApiVersion DefaultApiVersion = SharePointApiVersion.SP14;

		// Token: 0x04000E4F RID: 3663
		public const string DefaultViewMode = "All";

		// Token: 0x04000E50 RID: 3664
		public const string ImplementationTwoString = "2.0";

		// Token: 0x04000E51 RID: 3665
		private static readonly HashSet<string> validOptionKeysFolder = new HashSet<string> { "ApiVersion" };

		// Token: 0x04000E52 RID: 3666
		private static readonly HashSet<string> allValidOptionKeysTables = new HashSet<string> { "Implementation", "ApiVersion", "ViewMode", "DisableAppendNoteColumns" };

		// Token: 0x04000E53 RID: 3667
		private static readonly HashSet<string> allValidOptionKeysTablesImpl1 = new HashSet<string> { "Implementation", "ApiVersion" };
	}
}
