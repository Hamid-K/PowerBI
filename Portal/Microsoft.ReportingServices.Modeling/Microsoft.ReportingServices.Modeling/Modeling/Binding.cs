using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000060 RID: 96
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Binding : IPersistable
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000CEAD File Offset: 0x0000B0AD
		protected Binding(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000CECF File Offset: 0x0000B0CF
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x060003C9 RID: 969
		public abstract DsvTable GetTable();

		// Token: 0x060003CA RID: 970 RVA: 0x0000CED7 File Offset: 0x0000B0D7
		internal DataSourceView GetDataSourceView()
		{
			if (this.m_context == null)
			{
				return null;
			}
			return this.m_context.GetDataSourceView();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		internal Binding GetParentBinding()
		{
			if (this.m_context == null)
			{
				return null;
			}
			return this.m_context.GetParentBinding();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000CF08 File Offset: 0x0000B108
		internal DsvTable GetParentTable()
		{
			Binding parentBinding = this.GetParentBinding();
			if (parentBinding == null)
			{
				return null;
			}
			return parentBinding.GetTable();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000CF27 File Offset: 0x0000B127
		internal void SetContext(IBindingContext context)
		{
			this.m_context = context;
		}

		// Token: 0x060003CE RID: 974
		internal abstract void WriteTo(ModelingXmlWriter xw);

		// Token: 0x060003CF RID: 975 RVA: 0x0000CF30 File Offset: 0x0000B130
		protected bool CheckBinding()
		{
			DataSourceView dataSourceView;
			return this.CheckBinding(out dataSourceView);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000CF45 File Offset: 0x0000B145
		protected bool CheckBinding(out DataSourceView dataSourceView)
		{
			dataSourceView = this.GetDataSourceView();
			return dataSourceView != null;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000CF56 File Offset: 0x0000B156
		internal static bool CheckColumns(IList<DsvColumn> columns, Binding parentBinding)
		{
			return Binding.CheckColumns(columns, parentBinding, null, null);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000CF64 File Offset: 0x0000B164
		internal static bool CheckColumns(IList<DsvColumn> columns, Binding parentBinding, CompilationContext ctx, string propertyName)
		{
			if (columns == null)
			{
				throw new InternalModelingException("columns is null");
			}
			bool flag = true;
			if (parentBinding is ColumnBinding)
			{
				DsvColumn column = ((ColumnBinding)parentBinding).GetColumn();
				if (column != null)
				{
					foreach (DsvColumn dsvColumn in columns)
					{
						if (dsvColumn != column)
						{
							if (ctx != null)
							{
								ctx.AddScopedError(ModelingErrorCode.InvalidColumnReferenceInColumnEntity, SRErrors.InvalidColumnReferenceInColumnEntity(propertyName, ctx.CurrentObjectDescriptor, Binding.GetColumnDescriptor(dsvColumn.Table.Name, dsvColumn.Name), Binding.GetColumnDescriptor(column.Table.Name, column.Name)));
							}
							flag = false;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D018 File Offset: 0x0000B218
		internal static bool AreColumnsUnique(IList<DsvColumn> columns, Binding parentBinding)
		{
			if (columns == null)
			{
				throw new InternalModelingException("columns is null");
			}
			if (parentBinding is TableBinding)
			{
				DsvTable table = ((TableBinding)parentBinding).GetTable();
				if (table != null)
				{
					return table.AreColumnsUnique(columns);
				}
			}
			else if (parentBinding is ColumnBinding)
			{
				DsvColumn column = ((ColumnBinding)parentBinding).GetColumn();
				if (column != null)
				{
					return columns.Contains(column);
				}
			}
			return true;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D072 File Offset: 0x0000B272
		internal static SRObjectDescriptor GetTableDescriptor(string tableName)
		{
			return new SRObjectDescriptor("Table", tableName);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D07F File Offset: 0x0000B27F
		internal static SRObjectDescriptor GetColumnDescriptor(string tableName, string columnName)
		{
			return new SRObjectDescriptor("Column", string.IsNullOrEmpty(tableName) ? columnName : (tableName + "." + columnName));
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D0A2 File Offset: 0x0000B2A2
		internal static SRObjectDescriptor GetRelationDescriptor(string relationName)
		{
			return new SRObjectDescriptor("Relation", relationName);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		internal Binding()
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		internal virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Binding.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Name)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.Write(this.m_name);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D12C File Offset: 0x0000B32C
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D138 File Offset: 0x0000B338
		internal virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Binding.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.Name)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_name = reader.ReadString();
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060003DD RID: 989
		internal abstract void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x060003DE RID: 990 RVA: 0x0000D1AE File Offset: 0x0000B3AE
		ObjectType IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x060003DF RID: 991
		internal abstract ObjectType GetObjectType();

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D1B6 File Offset: 0x0000B3B6
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref Binding.__declaration, Binding.__declarationLock, () => new Declaration(ObjectType.Binding, ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Name, Token.String)
				}));
			}
		}

		// Token: 0x0400022A RID: 554
		internal const string NameAttr = "Name";

		// Token: 0x0400022B RID: 555
		private string m_name;

		// Token: 0x0400022C RID: 556
		private IBindingContext m_context;

		// Token: 0x0400022D RID: 557
		private static Declaration __declaration;

		// Token: 0x0400022E RID: 558
		private static readonly object __declarationLock = new object();
	}
}
