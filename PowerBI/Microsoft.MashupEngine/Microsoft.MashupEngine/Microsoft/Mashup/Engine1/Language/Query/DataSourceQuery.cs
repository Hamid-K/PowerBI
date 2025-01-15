using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001804 RID: 6148
	internal abstract class DataSourceQuery : Query
	{
		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x06009B72 RID: 39794 RVA: 0x00002105 File Offset: 0x00000305
		public sealed override QueryKind Kind
		{
			get
			{
				return QueryKind.DataSource;
			}
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		public override TypeValue GetColumnType(int column)
		{
			return TypeValue.Any;
		}

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x06009B74 RID: 39796 RVA: 0x00201A4A File Offset: 0x001FFC4A
		public override IList<TableKey> TableKeys
		{
			get
			{
				return new TableKey[0];
			}
		}

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x06009B75 RID: 39797 RVA: 0x00049E4C File Offset: 0x0004804C
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return new ComputedColumn[0];
			}
		}

		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x06009B76 RID: 39798 RVA: 0x00049E54 File Offset: 0x00048054
		public override TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x06009B77 RID: 39799 RVA: 0x001DEE24 File Offset: 0x001DD024
		public override IQueryDomain QueryDomain
		{
			get
			{
				return NullQueryDomain.Instance;
			}
		}

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x06009B78 RID: 39800
		public abstract IEngineHost EngineHost { get; }

		// Token: 0x06009B79 RID: 39801 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Query Unordered()
		{
			return this;
		}

		// Token: 0x06009B7A RID: 39802 RVA: 0x00201A54 File Offset: 0x001FFC54
		public virtual bool TryGetExpression(out IExpression expression)
		{
			INativeQueryDomain nativeQueryDomain = this.QueryDomain as INativeQueryDomain;
			IResource resource;
			Value value;
			RecordValue recordValue;
			if (nativeQueryDomain != null && nativeQueryDomain.TryGetNativeQuery(this, out resource, out value, out recordValue))
			{
				IDataSourceLocation dataSourceLocation;
				if (ResourceKinds.TryCreateLocationFromResource(resource, false, out dataSourceLocation))
				{
					string text = ((recordValue != null) ? Library.Text.FromBinary.Invoke(JsonModule.Json.FromValue.Invoke(recordValue)).AsString : null);
					FormulaCreationResult formulaCreationResult = dataSourceLocation.CreateFormula(text) as FormulaCreationResult;
					if (!formulaCreationResult.Success)
					{
						expression = null;
						return false;
					}
					expression = LibraryHelper.BindLibrary(this.GetEngineHost(), formulaCreationResult.FormulaExpression);
				}
				else
				{
					RecordValue recordValue2 = RecordValue.New(QueryToExpressionVisitor.ResourceKeys, new Value[]
					{
						TextValue.NewOrNull(resource.Kind),
						TextValue.NewOrNull(resource.Path)
					});
					expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(ResourceModule.Resource.Access), new IExpression[]
					{
						new ConstantExpressionSyntaxNode(recordValue2),
						ConstantExpressionSyntaxNode.Null,
						new ConstantExpressionSyntaxNode(recordValue)
					});
				}
				if (value == null || !value.IsNull)
				{
					expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library._Value.NativeQuery), new IExpression[]
					{
						expression,
						QueryToExpressionVisitor.NewNativeQueryExpression(value)
					});
				}
				return true;
			}
			expression = null;
			return false;
		}
	}
}
