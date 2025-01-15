using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.File
{
	// Token: 0x02000B99 RID: 2969
	internal static class FileSystemTableHelper
	{
		// Token: 0x060051AB RID: 20907 RVA: 0x00112732 File Offset: 0x00110932
		public static TableTypeValue AddTypeMetadata(TableTypeValue type, string rootPath, string pathSeparator, bool hierarchicalNavigation)
		{
			return FileSystemTableHelper.AddTypeMetadata(type, rootPath, pathSeparator, hierarchicalNavigation, "Name", "Content", "Folder Path");
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0011274C File Offset: 0x0011094C
		public static TableTypeValue AddTypeMetadata(TableTypeValue type, string rootPath, string pathSeparator, bool hierarchicalNavigation, string nameColumn, string contentColumn, string folderPathColumn)
		{
			if (!rootPath.EndsWith(pathSeparator, StringComparison.Ordinal))
			{
				rootPath += pathSeparator;
			}
			RecordValue metaValue = type.MetaValue;
			RecordValue recordValue = RecordValue.New(FileSystemTableHelper.TypeMetadataKeys, new Value[]
			{
				TextValue.New(rootPath),
				TextValue.New(pathSeparator),
				LogicalValue.New(hierarchicalNavigation),
				TextValue.New(nameColumn),
				TextValue.New(contentColumn),
				TextValue.New(folderPathColumn)
			});
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return type.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x04002C90 RID: 11408
		private const string RootPathKey = "FileSystemTable.RootPath";

		// Token: 0x04002C91 RID: 11409
		private const string PathSeparatorKey = "FileSystemTable.PathSeparator";

		// Token: 0x04002C92 RID: 11410
		private const string HierarchicalNavigationKey = "FileSystemTable.HierarchicalNavigation";

		// Token: 0x04002C93 RID: 11411
		private const string NameColumnKey = "FileSystemTable.NameColumn";

		// Token: 0x04002C94 RID: 11412
		private const string ContentColumnKey = "FileSystemTable.ContentColumn";

		// Token: 0x04002C95 RID: 11413
		private const string FolderPathColumnKey = "FileSystemTable.FolderPathColumn";

		// Token: 0x04002C96 RID: 11414
		private static readonly Keys TypeMetadataKeys = Keys.New(new string[] { "FileSystemTable.RootPath", "FileSystemTable.PathSeparator", "FileSystemTable.HierarchicalNavigation", "FileSystemTable.NameColumn", "FileSystemTable.ContentColumn", "FileSystemTable.FolderPathColumn" });
	}
}
