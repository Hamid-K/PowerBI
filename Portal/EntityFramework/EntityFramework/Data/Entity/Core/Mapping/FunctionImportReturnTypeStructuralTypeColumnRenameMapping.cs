using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000540 RID: 1344
	internal class FunctionImportReturnTypeStructuralTypeColumnRenameMapping
	{
		// Token: 0x060041E6 RID: 16870 RVA: 0x000DF2B4 File Offset: 0x000DD4B4
		internal FunctionImportReturnTypeStructuralTypeColumnRenameMapping(string defaultMemberName)
		{
			this._defaultMemberName = defaultMemberName;
			this._columnListForType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._columnListForIsTypeOfType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._renameCache = new Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(new Func<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(this.GetRename), EqualityComparer<StructuralType>.Default);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000DF300 File Offset: 0x000DD500
		internal string GetRename(EdmType type)
		{
			IXmlLineInfo xmlLineInfo;
			return this.GetRename(type, out xmlLineInfo);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000DF318 File Offset: 0x000DD518
		internal string GetRename(EdmType type, out IXmlLineInfo lineInfo)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = this._renameCache.Evaluate(type as StructuralType);
			lineInfo = functionImportReturnTypeStructuralTypeColumn.LineInfo;
			return functionImportReturnTypeStructuralTypeColumn.ColumnName;
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000DF348 File Offset: 0x000DD548
		private FunctionImportReturnTypeStructuralTypeColumn GetRename(StructuralType typeForRename)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = this._columnListForType.FirstOrDefault((FunctionImportReturnTypeStructuralTypeColumn t) => t.Type == typeForRename);
			if (functionImportReturnTypeStructuralTypeColumn != null)
			{
				return functionImportReturnTypeStructuralTypeColumn;
			}
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn2 = this._columnListForIsTypeOfType.Where((FunctionImportReturnTypeStructuralTypeColumn t) => t.Type == typeForRename).LastOrDefault<FunctionImportReturnTypeStructuralTypeColumn>();
			if (functionImportReturnTypeStructuralTypeColumn2 != null)
			{
				return functionImportReturnTypeStructuralTypeColumn2;
			}
			IEnumerable<FunctionImportReturnTypeStructuralTypeColumn> enumerable = this._columnListForIsTypeOfType.Where((FunctionImportReturnTypeStructuralTypeColumn t) => t.Type.IsAssignableFrom(typeForRename));
			if (enumerable.Count<FunctionImportReturnTypeStructuralTypeColumn>() == 0)
			{
				return new FunctionImportReturnTypeStructuralTypeColumn(this._defaultMemberName, typeForRename, false, null);
			}
			return FunctionImportReturnTypeStructuralTypeColumnRenameMapping.GetLowestParentInHierarchy(enumerable);
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000DF3DC File Offset: 0x000DD5DC
		private static FunctionImportReturnTypeStructuralTypeColumn GetLowestParentInHierarchy(IEnumerable<FunctionImportReturnTypeStructuralTypeColumn> nodesInHierarchy)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = null;
			foreach (FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn2 in nodesInHierarchy)
			{
				if (functionImportReturnTypeStructuralTypeColumn == null)
				{
					functionImportReturnTypeStructuralTypeColumn = functionImportReturnTypeStructuralTypeColumn2;
				}
				else if (functionImportReturnTypeStructuralTypeColumn.Type.IsAssignableFrom(functionImportReturnTypeStructuralTypeColumn2.Type))
				{
					functionImportReturnTypeStructuralTypeColumn = functionImportReturnTypeStructuralTypeColumn2;
				}
			}
			return functionImportReturnTypeStructuralTypeColumn;
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000DF43C File Offset: 0x000DD63C
		internal void AddRename(FunctionImportReturnTypeStructuralTypeColumn renamedColumn)
		{
			if (!renamedColumn.IsTypeOf)
			{
				this._columnListForType.Add(renamedColumn);
				return;
			}
			this._columnListForIsTypeOfType.Add(renamedColumn);
		}

		// Token: 0x040016DF RID: 5855
		private readonly Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForType;

		// Token: 0x040016E0 RID: 5856
		private readonly Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForIsTypeOfType;

		// Token: 0x040016E1 RID: 5857
		private readonly string _defaultMemberName;

		// Token: 0x040016E2 RID: 5858
		private readonly Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn> _renameCache;
	}
}
