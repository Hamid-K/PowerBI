using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039E RID: 926
	internal class Dump : BasicOpVisitor, IDisposable
	{
		// Token: 0x06002CFC RID: 11516 RVA: 0x000903D1 File Offset: 0x0008E5D1
		private Dump(Stream stream)
			: this(stream, Dump.DefaultEncoding)
		{
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000903E0 File Offset: 0x0008E5E0
		private Dump(Stream stream, Encoding encoding)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x00090427 File Offset: 0x0008E627
		internal static string ToXml(Command itree)
		{
			return Dump.ToXml(itree.Root);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x00090434 File Offset: 0x0008E634
		internal static string ToXml(Node subtree)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Dump dump = new Dump(memoryStream))
			{
				using (new Dump.AutoXml(dump, "nodes"))
				{
					dump.VisitNode(subtree);
				}
			}
			return Dump.DefaultEncoding.GetString(memoryStream.ToArray());
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000904A8 File Offset: 0x0008E6A8
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			try
			{
				this._writer.WriteEndDocument();
				this._writer.Flush();
				this._writer.Close();
			}
			catch (Exception ex)
			{
				if (!ex.IsCatchableExceptionType())
				{
					throw;
				}
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000904FC File Offset: 0x0008E6FC
		internal void Begin(string name, Dictionary<string, object> attrs)
		{
			this._writer.WriteStartElement(name);
			if (attrs != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					this._writer.WriteAttributeString(keyValuePair.Key, keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x00090570 File Offset: 0x0008E770
		internal void BeginExpression()
		{
			this.WriteString("(");
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0009057D File Offset: 0x0008E77D
		internal void EndExpression()
		{
			this.WriteString(")");
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0009058A File Offset: 0x0008E78A
		internal void End()
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x00090597 File Offset: 0x0008E797
		internal void WriteString(string value)
		{
			this._writer.WriteString(value);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000905A8 File Offset: 0x0008E7A8
		protected override void VisitDefault(Node n)
		{
			using (new Dump.AutoXml(this, n.Op))
			{
				base.VisitDefault(n);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000905EC File Offset: 0x0008E7EC
		protected override void VisitScalarOpDefault(ScalarOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				string text = string.Empty;
				foreach (Node node in n.Children)
				{
					this.WriteString(text);
					this.VisitNode(node);
					text = ",";
				}
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x00090678 File Offset: 0x0008E878
		protected override void VisitJoinOp(JoinBaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				if (n.Children.Count > 2)
				{
					using (new Dump.AutoXml(this, "condition"))
					{
						this.VisitNode(n.Child2);
					}
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitNode(n.Child0);
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitNode(n.Child1);
				}
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x00090758 File Offset: 0x0008E958
		public override void Visit(CaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				int i = 0;
				while (i < n.Children.Count)
				{
					if (i + 1 < n.Children.Count)
					{
						using (new Dump.AutoXml(this, "when"))
						{
							this.VisitNode(n.Children[i++]);
						}
						using (new Dump.AutoXml(this, "then"))
						{
							this.VisitNode(n.Children[i++]);
							continue;
						}
					}
					using (new Dump.AutoXml(this, "else"))
					{
						this.VisitNode(n.Children[i++]);
					}
				}
			}
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x00090870 File Offset: 0x0008EA70
		public override void Visit(CollectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000908B0 File Offset: 0x0008EAB0
		protected override void VisitConstantOp(ConstantBaseOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				if (op.Value == null)
				{
					this.WriteString("null");
				}
				else
				{
					this.WriteString("(");
					this.WriteString(op.Type.EdmType.FullName);
					this.WriteString(")");
					this.WriteString(string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { op.Value }));
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x00090954 File Offset: 0x0008EB54
		public override void Visit(DistinctOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in op.Keys)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(var.Id);
				text = ",";
			}
			if (stringBuilder.Length != 0)
			{
				dictionary.Add("Keys", stringBuilder.ToString());
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x00090A10 File Offset: 0x0008EC10
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in op.Keys)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(var.Id);
				text = ",";
			}
			if (stringBuilder.Length != 0)
			{
				dictionary.Add("Keys", stringBuilder.ToString());
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var var2 in op.Outputs)
					{
						this.DumpVar(var2);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x00090B30 File Offset: 0x0008ED30
		public override void Visit(IsOfOp op, Node n)
		{
			using (new Dump.AutoXml(this, op.IsOfOnly ? "IsOfOnly" : "IsOf"))
			{
				string text = string.Empty;
				foreach (Node node in n.Children)
				{
					this.WriteString(text);
					this.VisitNode(node);
					text = ",";
				}
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x00090BCC File Offset: 0x0008EDCC
		protected override void VisitNestOp(NestBaseOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			SingleStreamNestOp singleStreamNestOp = op as SingleStreamNestOp;
			if (singleStreamNestOp != null)
			{
				dictionary.Add("Discriminator", (singleStreamNestOp.Discriminator == null) ? "<null>" : singleStreamNestOp.Discriminator.ToString());
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (singleStreamNestOp != null)
			{
				stringBuilder.Length = 0;
				string text = string.Empty;
				foreach (Var var in singleStreamNestOp.Keys)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(var.Id);
					text = ",";
				}
				if (stringBuilder.Length != 0)
				{
					dictionary.Add("Keys", stringBuilder.ToString());
				}
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var var2 in op.Outputs)
					{
						this.DumpVar(var2);
					}
				}
				foreach (CollectionInfo collectionInfo in op.CollectionInfo)
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2.Add("CollectionVar", collectionInfo.CollectionVar);
					if (collectionInfo.DiscriminatorValue != null)
					{
						dictionary2.Add("DiscriminatorValue", collectionInfo.DiscriminatorValue);
					}
					if (collectionInfo.FlattenedElementVars.Count != 0)
					{
						dictionary2.Add("FlattenedElementVars", Dump.FormatVarList(stringBuilder, collectionInfo.FlattenedElementVars));
					}
					if (collectionInfo.Keys.Count != 0)
					{
						dictionary2.Add("Keys", collectionInfo.Keys);
					}
					if (collectionInfo.SortKeys.Count != 0)
					{
						dictionary2.Add("SortKeys", Dump.FormatVarList(stringBuilder, collectionInfo.SortKeys));
					}
					using (new Dump.AutoXml(this, "collection", dictionary2))
					{
						collectionInfo.ColumnMap.Accept<Dump>(Dump.ColumnMapDumper.Instance, this);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x00090E9C File Offset: 0x0008F09C
		private static string FormatVarList(StringBuilder sb, VarList varList)
		{
			sb.Length = 0;
			string text = string.Empty;
			foreach (Var var in varList)
			{
				sb.Append(text);
				sb.Append(var.Id);
				text = ",";
			}
			return sb.ToString();
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x00090F14 File Offset: 0x0008F114
		private static string FormatVarList(StringBuilder sb, List<SortKey> varList)
		{
			sb.Length = 0;
			string text = string.Empty;
			foreach (SortKey sortKey in varList)
			{
				sb.Append(text);
				sb.Append(sortKey.Var.Id);
				text = ",";
			}
			return sb.ToString();
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x00090F90 File Offset: 0x0008F190
		private void VisitNewOp(Op op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				foreach (Node node in n.Children)
				{
					using (new Dump.AutoXml(this, "argument", null))
					{
						this.VisitNode(node);
					}
				}
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x00091030 File Offset: 0x0008F230
		public override void Visit(NewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0009103A File Offset: 0x0008F23A
		public override void Visit(NewInstanceOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x00091044 File Offset: 0x0008F244
		public override void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0009104E File Offset: 0x0008F24E
		public override void Visit(NewMultisetOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x00091058 File Offset: 0x0008F258
		public override void Visit(NewRecordOp op, Node n)
		{
			this.VisitNewOp(op, n);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00091064 File Offset: 0x0008F264
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var var in op.Outputs)
					{
						this.DumpVar(var);
					}
				}
				using (new Dump.AutoXml(this, "columnMap"))
				{
					op.ColumnMap.Accept<Dump>(Dump.ColumnMapDumper.Instance, this);
				}
				using (new Dump.AutoXml(this, "input"))
				{
					this.VisitChildren(n);
				}
			}
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x00091168 File Offset: 0x0008F368
		public override void Visit(ProjectOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var var in op.Outputs)
					{
						this.DumpVar(var);
					}
				}
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x00091208 File Offset: 0x0008F408
		public override void Visit(PropertyOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				this.WriteString(".");
				this.WriteString(op.PropertyInfo.Name);
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x00091264 File Offset: 0x0008F464
		public override void Visit(RelPropertyOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				this.WriteString(".NAVIGATE(");
				this.WriteString(op.PropertyInfo.Relationship.Name);
				this.WriteString(",");
				this.WriteString(op.PropertyInfo.FromEnd.Name);
				this.WriteString(",");
				this.WriteString(op.PropertyInfo.ToEnd.Name);
				this.WriteString(")");
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x00091310 File Offset: 0x0008F510
		public override void Visit(ScanTableOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0009135C File Offset: 0x0008F55C
		public override void Visit(ScanViewOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000913A8 File Offset: 0x0008F5A8
		protected override void VisitSetOp(SetOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (OpType.UnionAll == op.OpType)
			{
				UnionAllOp unionAllOp = (UnionAllOp)op;
				if (unionAllOp.BranchDiscriminator != null)
				{
					dictionary.Add("branchDiscriminator", unionAllOp.BranchDiscriminator);
				}
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				using (new Dump.AutoXml(this, "outputs"))
				{
					foreach (Var var in op.Outputs)
					{
						this.DumpVar(var);
					}
				}
				int num = 0;
				foreach (Node node in n.Children)
				{
					using (new Dump.AutoXml(this, "input", new Dictionary<string, object> { 
					{
						"VarMap",
						op.VarMap[num++].ToString()
					} }))
					{
						this.VisitNode(node);
					}
				}
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x00091510 File Offset: 0x0008F710
		public override void Visit(SortOp op, Node n)
		{
			using (new Dump.AutoXml(this, op))
			{
				base.Visit(op, n);
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x00091550 File Offset: 0x0008F750
		public override void Visit(ConstrainedSortOp op, Node n)
		{
			using (new Dump.AutoXml(this, op, new Dictionary<string, object> { { "WithTies", op.WithTies } }))
			{
				base.Visit(op, n);
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000915AC File Offset: 0x0008F7AC
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			using (new Dump.AutoXml(this, "keys"))
			{
				foreach (SortKey sortKey in op.Keys)
				{
					using (new Dump.AutoXml(this, "sortKey", new Dictionary<string, object>
					{
						{ "Var", sortKey.Var },
						{ "Ascending", sortKey.AscendingSort },
						{ "Collation", sortKey.Collation }
					}))
					{
					}
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x00091690 File Offset: 0x0008F890
		public override void Visit(UnnestOp op, Node n)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (op.Var != null)
			{
				dictionary.Add("Var", op.Var.Id);
			}
			using (new Dump.AutoXml(this, op, dictionary))
			{
				this.DumpTable(op.Table);
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x00091704 File Offset: 0x0008F904
		public override void Visit(VarDefOp op, Node n)
		{
			using (new Dump.AutoXml(this, op, new Dictionary<string, object> { 
			{
				"Var",
				op.Var.Id
			} }))
			{
				this.VisitChildren(n);
			}
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x00091764 File Offset: 0x0008F964
		public override void Visit(VarRefOp op, Node n)
		{
			using (new Dump.AutoString(this, op))
			{
				this.VisitChildren(n);
				if (op.Type != null)
				{
					this.WriteString("Type=");
					this.WriteString(op.Type.ToString());
					this.WriteString(", ");
				}
				this.WriteString("Var=");
				this.WriteString(op.Var.Id.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000917FC File Offset: 0x0008F9FC
		private void DumpVar(Var v)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Var", v.Id);
			ColumnVar columnVar = v as ColumnVar;
			if (columnVar != null)
			{
				dictionary.Add("Name", columnVar.ColumnMetadata.Name);
				dictionary.Add("Type", columnVar.ColumnMetadata.Type.ToString());
			}
			using (new Dump.AutoXml(this, v.GetType().Name, dictionary))
			{
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x00091894 File Offset: 0x0008FA94
		private void DumpVars(List<Var> vars)
		{
			foreach (Var var in vars)
			{
				this.DumpVar(var);
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000918E4 File Offset: 0x0008FAE4
		private void DumpTable(Table table)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Table", table.TableId);
			if (table.TableMetadata.Extent != null)
			{
				dictionary.Add("Extent", table.TableMetadata.Extent.Name);
			}
			using (new Dump.AutoXml(this, "Table", dictionary))
			{
				this.DumpVars(table.Columns);
			}
		}

		// Token: 0x04000F1C RID: 3868
		private readonly XmlWriter _writer;

		// Token: 0x04000F1D RID: 3869
		internal static readonly Encoding DefaultEncoding = Encoding.UTF8;

		// Token: 0x02000A02 RID: 2562
		internal class ColumnMapDumper : ColumnMapVisitor<Dump>
		{
			// Token: 0x06006069 RID: 24681 RVA: 0x0014B183 File Offset: 0x00149383
			private ColumnMapDumper()
			{
			}

			// Token: 0x0600606A RID: 24682 RVA: 0x0014B18C File Offset: 0x0014938C
			private void DumpCollection(CollectionColumnMap columnMap, Dump dumper)
			{
				if (columnMap.ForeignKeys.Length != 0)
				{
					using (new Dump.AutoXml(dumper, "foreignKeys"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, dumper);
					}
				}
				if (columnMap.Keys.Length != 0)
				{
					using (new Dump.AutoXml(dumper, "keys"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.Keys, dumper);
					}
				}
				using (new Dump.AutoXml(dumper, "element"))
				{
					columnMap.Element.Accept<Dump>(this, dumper);
				}
			}

			// Token: 0x0600606B RID: 24683 RVA: 0x0014B254 File Offset: 0x00149454
			private static Dictionary<string, object> GetAttributes(ColumnMap columnMap)
			{
				return new Dictionary<string, object> { 
				{
					"Type",
					columnMap.Type.ToString()
				} };
			}

			// Token: 0x0600606C RID: 24684 RVA: 0x0014B274 File Offset: 0x00149474
			internal override void Visit(ComplexTypeColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "ComplexType", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					if (columnMap.NullSentinel != null)
					{
						using (new Dump.AutoXml(dumper, "nullSentinel"))
						{
							columnMap.NullSentinel.Accept<Dump>(this, dumper);
						}
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x0600606D RID: 24685 RVA: 0x0014B300 File Offset: 0x00149500
			internal override void Visit(DiscriminatedCollectionColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "DiscriminatedCollection", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "discriminator", new Dictionary<string, object> { { "Value", columnMap.DiscriminatorValue } }))
					{
						columnMap.Discriminator.Accept<Dump>(this, dumper);
					}
					this.DumpCollection(columnMap, dumper);
				}
			}

			// Token: 0x0600606E RID: 24686 RVA: 0x0014B398 File Offset: 0x00149598
			internal override void Visit(EntityColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Entity", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "entityIdentity"))
					{
						base.VisitEntityIdentity(columnMap.EntityIdentity, dumper);
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x0600606F RID: 24687 RVA: 0x0014B41C File Offset: 0x0014961C
			internal override void Visit(SimplePolymorphicColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimplePolymorphic", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "typeDiscriminator"))
					{
						columnMap.TypeDiscriminator.Accept<Dump>(this, dumper);
					}
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
					{
						dictionary.Clear();
						dictionary.Add("DiscriminatorValue", keyValuePair.Key);
						using (new Dump.AutoXml(dumper, "choice", dictionary))
						{
							keyValuePair.Value.Accept<Dump>(this, dumper);
						}
					}
					using (new Dump.AutoXml(dumper, "default"))
					{
						base.VisitList<ColumnMap>(columnMap.Properties, dumper);
					}
				}
			}

			// Token: 0x06006070 RID: 24688 RVA: 0x0014B558 File Offset: 0x00149758
			internal override void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "MultipleDiscriminatorPolymorphic", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "typeDiscriminators"))
					{
						base.VisitList<SimpleColumnMap>(columnMap.TypeDiscriminators, dumper);
					}
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
					{
						dictionary.Clear();
						dictionary.Add("EntityType", keyValuePair.Key);
						using (new Dump.AutoXml(dumper, "choice", dictionary))
						{
							keyValuePair.Value.Accept<Dump>(this, dumper);
						}
					}
					using (new Dump.AutoXml(dumper, "default"))
					{
						base.VisitList<ColumnMap>(columnMap.Properties, dumper);
					}
				}
			}

			// Token: 0x06006071 RID: 24689 RVA: 0x0014B694 File Offset: 0x00149894
			internal override void Visit(RecordColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Record", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					if (columnMap.NullSentinel != null)
					{
						using (new Dump.AutoXml(dumper, "nullSentinel"))
						{
							columnMap.NullSentinel.Accept<Dump>(this, dumper);
						}
					}
					base.VisitList<ColumnMap>(columnMap.Properties, dumper);
				}
			}

			// Token: 0x06006072 RID: 24690 RVA: 0x0014B720 File Offset: 0x00149920
			internal override void Visit(RefColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "Ref", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					using (new Dump.AutoXml(dumper, "entityIdentity"))
					{
						base.VisitEntityIdentity(columnMap.EntityIdentity, dumper);
					}
				}
			}

			// Token: 0x06006073 RID: 24691 RVA: 0x0014B794 File Offset: 0x00149994
			internal override void Visit(SimpleCollectionColumnMap columnMap, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleCollection", Dump.ColumnMapDumper.GetAttributes(columnMap)))
				{
					this.DumpCollection(columnMap, dumper);
				}
			}

			// Token: 0x06006074 RID: 24692 RVA: 0x0014B7DC File Offset: 0x001499DC
			internal override void Visit(ScalarColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("CommandId", columnMap.CommandId);
				attributes.Add("ColumnPos", columnMap.ColumnPos);
				using (new Dump.AutoXml(dumper, "AssignedSimple", attributes))
				{
				}
			}

			// Token: 0x06006075 RID: 24693 RVA: 0x0014B84C File Offset: 0x00149A4C
			internal override void Visit(VarRefColumnMap columnMap, Dump dumper)
			{
				Dictionary<string, object> attributes = Dump.ColumnMapDumper.GetAttributes(columnMap);
				attributes.Add("Var", columnMap.Var.Id);
				using (new Dump.AutoXml(dumper, "VarRef", attributes))
				{
				}
			}

			// Token: 0x06006076 RID: 24694 RVA: 0x0014B8AC File Offset: 0x00149AAC
			protected override void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "DiscriminatedEntityIdentity"))
				{
					using (new Dump.AutoXml(dumper, "entitySetId"))
					{
						entityIdentity.EntitySetColumnMap.Accept<Dump>(this, dumper);
					}
					if (entityIdentity.Keys.Length != 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x06006077 RID: 24695 RVA: 0x0014B95C File Offset: 0x00149B5C
			protected override void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, Dump dumper)
			{
				using (new Dump.AutoXml(dumper, "SimpleEntityIdentity"))
				{
					if (entityIdentity.Keys.Length != 0)
					{
						using (new Dump.AutoXml(dumper, "keys"))
						{
							base.VisitList<SimpleColumnMap>(entityIdentity.Keys, dumper);
						}
					}
				}
			}

			// Token: 0x040028F9 RID: 10489
			internal static Dump.ColumnMapDumper Instance = new Dump.ColumnMapDumper();
		}

		// Token: 0x02000A03 RID: 2563
		internal struct AutoString : IDisposable
		{
			// Token: 0x06006079 RID: 24697 RVA: 0x0014B9E0 File Offset: 0x00149BE0
			internal AutoString(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._dumper.WriteString(Dump.AutoString.ToString(op.OpType));
				this._dumper.BeginExpression();
			}

			// Token: 0x0600607A RID: 24698 RVA: 0x0014BA0C File Offset: 0x00149C0C
			public void Dispose()
			{
				try
				{
					this._dumper.EndExpression();
				}
				catch (Exception ex)
				{
					if (!ex.IsCatchableExceptionType())
					{
						throw;
					}
				}
			}

			// Token: 0x0600607B RID: 24699 RVA: 0x0014BA44 File Offset: 0x00149C44
			internal static string ToString(OpType op)
			{
				switch (op)
				{
				case OpType.Constant:
					return "Constant";
				case OpType.InternalConstant:
					return "InternalConstant";
				case OpType.NullSentinel:
					return "NullSentinel";
				case OpType.Null:
					return "Null";
				case OpType.ConstantPredicate:
					return "ConstantPredicate";
				case OpType.VarRef:
					return "VarRef";
				case OpType.GT:
					return "GT";
				case OpType.GE:
					return "GE";
				case OpType.LE:
					return "LE";
				case OpType.LT:
					return "LT";
				case OpType.EQ:
					return "EQ";
				case OpType.NE:
					return "NE";
				case OpType.Like:
					return "Like";
				case OpType.Plus:
					return "Plus";
				case OpType.Minus:
					return "Minus";
				case OpType.Multiply:
					return "Multiply";
				case OpType.Divide:
					return "Divide";
				case OpType.Modulo:
					return "Modulo";
				case OpType.UnaryMinus:
					return "UnaryMinus";
				case OpType.And:
					return "And";
				case OpType.Or:
					return "Or";
				case OpType.In:
					return "In";
				case OpType.Not:
					return "Not";
				case OpType.IsNull:
					return "IsNull";
				case OpType.Case:
					return "Case";
				case OpType.Treat:
					return "Treat";
				case OpType.IsOf:
					return "IsOf";
				case OpType.Cast:
					return "Cast";
				case OpType.SoftCast:
					return "SoftCast";
				case OpType.Aggregate:
					return "Aggregate";
				case OpType.Function:
					return "Function";
				case OpType.RelProperty:
					return "RelProperty";
				case OpType.Property:
					return "Property";
				case OpType.NewEntity:
					return "NewEntity";
				case OpType.NewInstance:
					return "NewInstance";
				case OpType.DiscriminatedNewEntity:
					return "DiscriminatedNewEntity";
				case OpType.NewMultiset:
					return "NewMultiset";
				case OpType.NewRecord:
					return "NewRecord";
				case OpType.GetRefKey:
					return "GetRefKey";
				case OpType.GetEntityRef:
					return "GetEntityRef";
				case OpType.Ref:
					return "Ref";
				case OpType.Exists:
					return "Exists";
				case OpType.Element:
					return "Element";
				case OpType.Collect:
					return "Collect";
				case OpType.Deref:
					return "Deref";
				case OpType.Navigate:
					return "Navigate";
				case OpType.ScanTable:
					return "ScanTable";
				case OpType.ScanView:
					return "ScanView";
				case OpType.Filter:
					return "Filter";
				case OpType.Project:
					return "Project";
				case OpType.InnerJoin:
					return "InnerJoin";
				case OpType.LeftOuterJoin:
					return "LeftOuterJoin";
				case OpType.FullOuterJoin:
					return "FullOuterJoin";
				case OpType.CrossJoin:
					return "CrossJoin";
				case OpType.CrossApply:
					return "CrossApply";
				case OpType.OuterApply:
					return "OuterApply";
				case OpType.Unnest:
					return "Unnest";
				case OpType.Sort:
					return "Sort";
				case OpType.ConstrainedSort:
					return "ConstrainedSort";
				case OpType.GroupBy:
					return "GroupBy";
				case OpType.GroupByInto:
					return "GroupByInto";
				case OpType.UnionAll:
					return "UnionAll";
				case OpType.Intersect:
					return "Intersect";
				case OpType.Except:
					return "Except";
				case OpType.Distinct:
					return "Distinct";
				case OpType.SingleRow:
					return "SingleRow";
				case OpType.SingleRowTable:
					return "SingleRowTable";
				case OpType.VarDef:
					return "VarDef";
				case OpType.VarDefList:
					return "VarDefList";
				case OpType.Leaf:
					return "Leaf";
				case OpType.PhysicalProject:
					return "PhysicalProject";
				case OpType.SingleStreamNest:
					return "SingleStreamNest";
				case OpType.MultiStreamNest:
					return "MultiStreamNest";
				default:
					return op.ToString();
				}
			}

			// Token: 0x040028FA RID: 10490
			private readonly Dump _dumper;
		}

		// Token: 0x02000A04 RID: 2564
		internal struct AutoXml : IDisposable
		{
			// Token: 0x0600607C RID: 24700 RVA: 0x0014BD44 File Offset: 0x00149F44
			internal AutoXml(Dump dumper, Op op)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", op.Type.ToString());
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x0600607D RID: 24701 RVA: 0x0014BDA0 File Offset: 0x00149FA0
			internal AutoXml(Dump dumper, Op op, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = Dump.AutoString.ToString(op.OpType);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (op.Type != null)
				{
					dictionary.Add("Type", op.Type.ToString());
				}
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				this._dumper.Begin(this._nodeName, dictionary);
			}

			// Token: 0x0600607E RID: 24702 RVA: 0x0014BE4C File Offset: 0x0014A04C
			internal AutoXml(Dump dumper, string nodeName)
			{
				this = new Dump.AutoXml(dumper, nodeName, null);
			}

			// Token: 0x0600607F RID: 24703 RVA: 0x0014BE57 File Offset: 0x0014A057
			internal AutoXml(Dump dumper, string nodeName, Dictionary<string, object> attrs)
			{
				this._dumper = dumper;
				this._nodeName = nodeName;
				this._dumper.Begin(this._nodeName, attrs);
			}

			// Token: 0x06006080 RID: 24704 RVA: 0x0014BE79 File Offset: 0x0014A079
			public void Dispose()
			{
				this._dumper.End();
			}

			// Token: 0x040028FB RID: 10491
			private readonly string _nodeName;

			// Token: 0x040028FC RID: 10492
			private readonly Dump _dumper;
		}
	}
}
