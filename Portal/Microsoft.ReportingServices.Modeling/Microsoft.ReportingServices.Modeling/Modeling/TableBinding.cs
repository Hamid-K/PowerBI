using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000061 RID: 97
	public sealed class TableBinding : Binding
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000D1F2 File Offset: 0x0000B3F2
		public TableBinding(string name)
			: base(name)
		{
			this.Init(ref this.m_cachedDsvTable);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D207 File Offset: 0x0000B407
		private void Init(ref CachedDsvItem<DsvTable> cachedDsvTable)
		{
			cachedDsvTable = new CachedDsvItem<DsvTable>(new CreatorGetter<DataSourceView>(base.GetDataSourceView), (DataSourceView dsv) => dsv.Tables[base.Name]);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D228 File Offset: 0x0000B428
		public override DsvTable GetTable()
		{
			return this.m_cachedDsvTable.GetItem();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000D238 File Offset: 0x0000B438
		internal static TableBinding FromReader(ModelingXmlReader xr)
		{
			string text = null;
			xr.CheckElement("Table");
			if (xr.MoveToAttribute("Name"))
			{
				text = xr.ReadValueAsString();
				xr.MoveToElement();
			}
			xr.Skip();
			return new TableBinding(text);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D279 File Offset: 0x0000B479
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Table");
			xw.WriteAttribute("Name", base.Name);
			xw.WriteEndElement();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		internal bool CheckBinding(CompilationContext ctx, out DsvTable table)
		{
			table = null;
			DataSourceView dataSourceView;
			if (!base.CheckBinding(out dataSourceView))
			{
				return false;
			}
			table = this.GetTable();
			if (table == null)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidBinding, SRErrors.InvalidBinding("Table", ctx.CurrentObjectDescriptor, this.GetTableDescriptor()));
				return false;
			}
			bool flag = false;
			if (table.DataSourceID != null && table.DataSourceID != dataSourceView.DataSourceID)
			{
				ctx.AddScopedError(ModelingErrorCode.NonPrimaryDataSource, SRErrors.NonPrimaryDataSource("Table", ctx.CurrentObjectDescriptor, this.GetTableDescriptor()));
				flag = true;
			}
			if (table.PrimaryKey.Count == 0)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingPrimaryKey, SRErrors.MissingPrimaryKey("Table", ctx.CurrentObjectDescriptor, this.GetTableDescriptor()));
				flag = true;
			}
			else
			{
				foreach (DsvColumn dsvColumn in table.PrimaryKey)
				{
					if (dsvColumn.ModelingDataType == null)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidColumnDataType, SRErrors.InvalidColumnDataType("PrimaryKey", this.GetTableDescriptor(), Binding.GetColumnDescriptor(table.Name, dsvColumn.Name)));
						flag = true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		internal SRObjectDescriptor GetTableDescriptor()
		{
			return Binding.GetTableDescriptor(base.Name);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D3E1 File Offset: 0x0000B5E1
		internal static IEnumerable<TableBinding> ListBindings(DataSourceView dataSourceView)
		{
			if (dataSourceView == null)
			{
				yield break;
			}
			foreach (DsvTable dsvTable in dataSourceView.Tables)
			{
				yield return new TableBinding(dsvTable.Name);
			}
			IndirectReadOnlyCollection<DsvTable>.Enumerator enumerator = default(IndirectReadOnlyCollection<DsvTable>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
		internal TableBinding()
		{
			this.Init(ref this.m_cachedDsvTable);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D408 File Offset: 0x0000B608
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TableBinding.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D46C File Offset: 0x0000B66C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TableBinding.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		internal override ObjectType GetObjectType()
		{
			return ObjectType.TableBinding;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref TableBinding.__declaration, TableBinding.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.TableBinding, ObjectType.Binding, list);
				});
			}
		}

		// Token: 0x0400022F RID: 559
		internal const string TableElem = "Table";

		// Token: 0x04000230 RID: 560
		private readonly CachedDsvItem<DsvTable> m_cachedDsvTable;

		// Token: 0x04000231 RID: 561
		private static Declaration __declaration;

		// Token: 0x04000232 RID: 562
		private static readonly object __declarationLock = new object();
	}
}
