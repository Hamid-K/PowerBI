using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000062 RID: 98
	public sealed class ColumnBinding : Binding
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D52D File Offset: 0x0000B72D
		public ColumnBinding(string name)
			: this(name, null)
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D537 File Offset: 0x0000B737
		public ColumnBinding(string name, string tableName)
			: base(name)
		{
			this.m_tableName = tableName ?? string.Empty;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000D550 File Offset: 0x0000B750
		public string TableName
		{
			get
			{
				return this.m_tableName;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D558 File Offset: 0x0000B758
		public DsvColumn GetColumn()
		{
			DsvTable table = this.GetTable();
			if (table != null)
			{
				return table.Columns[base.Name];
			}
			return null;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D584 File Offset: 0x0000B784
		public override DsvTable GetTable()
		{
			if (this.m_tableName.Length <= 0)
			{
				return base.GetParentTable();
			}
			DataSourceView dataSourceView = base.GetDataSourceView();
			if (dataSourceView == null)
			{
				return null;
			}
			return dataSourceView.Tables[this.m_tableName];
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		internal static ColumnBinding FromReader(ModelingXmlReader xr)
		{
			string text = null;
			string text2 = null;
			xr.CheckElement("Column");
			if (xr.MoveToAttribute("Name"))
			{
				text = xr.ReadValueAsString();
				xr.MoveToElement();
			}
			if (xr.MoveToAttribute("TableName"))
			{
				text2 = xr.ReadValueAsString();
				xr.MoveToElement();
			}
			xr.Skip();
			return new ColumnBinding(text, text2);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D623 File Offset: 0x0000B823
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Column");
			xw.WriteAttributeIfNonDefault<string>("TableName", this.m_tableName);
			xw.WriteAttribute("Name", base.Name);
			xw.WriteEndElement();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D658 File Offset: 0x0000B858
		internal bool CheckBinding(CompilationContext ctx, bool topLevel, out DsvColumn column, out DataType columnDataType)
		{
			column = null;
			columnDataType = DataType.Null;
			if (!base.CheckBinding())
			{
				return false;
			}
			if (topLevel)
			{
				if (this.m_tableName.Length == 0)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingColumnTableName, SRErrors.MissingColumnTableName(ctx.CurrentObjectDescriptor));
					return false;
				}
				if (this.GetTable() == null)
				{
					ctx.AddScopedError(ModelingErrorCode.InvalidBinding, SRErrors.InvalidBinding("Column", ctx.CurrentObjectDescriptor, Binding.GetTableDescriptor(this.m_tableName)));
					return false;
				}
			}
			else
			{
				DsvTable parentTable = base.GetParentTable();
				DsvTable table = this.GetTable();
				if (parentTable == null)
				{
					return false;
				}
				if (table != parentTable)
				{
					ctx.AddScopedError(ModelingErrorCode.InvalidColumnTableName, SRErrors.InvalidColumnTableName(ctx.CurrentObjectDescriptor, Binding.GetTableDescriptor(this.m_tableName), Binding.GetTableDescriptor(parentTable.Name)));
				}
			}
			column = this.GetColumn();
			if (column == null)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidBinding, SRErrors.InvalidBinding("Column", ctx.CurrentObjectDescriptor, this.GetColumnDescriptor()));
				return false;
			}
			if (!topLevel && !Binding.CheckColumns(new DsvColumn[] { column }, base.GetParentBinding(), ctx, "Column"))
			{
				return false;
			}
			if (column.ModelingDataType == null)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidColumnDataType, SRErrors.InvalidColumnDataType("Column", ctx.CurrentObjectDescriptor, this.GetColumnDescriptor()));
				return false;
			}
			columnDataType = column.ModelingDataType.Value;
			return true;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D79C File Offset: 0x0000B99C
		internal SRObjectDescriptor GetColumnDescriptor()
		{
			string text;
			if (this.m_tableName.Length > 0)
			{
				text = this.m_tableName;
			}
			else
			{
				DsvTable table = this.GetTable();
				text = ((table != null) ? table.Name : null);
			}
			return Binding.GetColumnDescriptor(text, base.Name);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		internal static IEnumerable<ColumnBinding> ListBindings(DsvTable table)
		{
			if (table == null)
			{
				yield break;
			}
			foreach (DsvColumn dsvColumn in table.Columns)
			{
				yield return new ColumnBinding(dsvColumn.Name, table.Name);
			}
			IndirectReadOnlyCollection<DsvColumn>.Enumerator enumerator = default(IndirectReadOnlyCollection<DsvColumn>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		internal static IEnumerable<ColumnBinding> ListBindings(Binding parentBinding)
		{
			if (parentBinding == null)
			{
				yield break;
			}
			DsvTable table = parentBinding.GetTable();
			if (table == null)
			{
				yield break;
			}
			foreach (DsvColumn dsvColumn in table.Columns)
			{
				if (Binding.CheckColumns(new DsvColumn[] { dsvColumn }, parentBinding))
				{
					yield return new ColumnBinding(dsvColumn.Name);
				}
			}
			IndirectReadOnlyCollection<DsvColumn>.Enumerator enumerator = default(IndirectReadOnlyCollection<DsvColumn>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000D800 File Offset: 0x0000BA00
		internal ColumnBinding()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000D808 File Offset: 0x0000BA08
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ColumnBinding.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.TableName)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.Write(this.m_tableName);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000D87C File Offset: 0x0000BA7C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ColumnBinding.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.TableName)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_tableName = reader.PersistenceHelper.NameTable.Add(reader.ReadString());
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000D901 File Offset: 0x0000BB01
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000D90D File Offset: 0x0000BB0D
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ColumnBinding;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000D911 File Offset: 0x0000BB11
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ColumnBinding.__declaration, ColumnBinding.__declarationLock, () => new Declaration(ObjectType.ColumnBinding, ObjectType.Binding, new List<MemberInfo>
				{
					new MemberInfo(MemberName.TableName, Token.String)
				}));
			}
		}

		// Token: 0x04000233 RID: 563
		internal const string ColumnElem = "Column";

		// Token: 0x04000234 RID: 564
		private const string TableNameAttr = "TableName";

		// Token: 0x04000235 RID: 565
		private string m_tableName;

		// Token: 0x04000236 RID: 566
		private static Declaration __declaration;

		// Token: 0x04000237 RID: 567
		private static readonly object __declarationLock = new object();
	}
}
