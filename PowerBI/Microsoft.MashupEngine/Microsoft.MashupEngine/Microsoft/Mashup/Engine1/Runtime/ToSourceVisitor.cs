using System;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001675 RID: 5749
	internal class ToSourceVisitor : ValueVisitor
	{
		// Token: 0x06009182 RID: 37250 RVA: 0x001E35D7 File Offset: 0x001E17D7
		private ToSourceVisitor()
		{
		}

		// Token: 0x06009183 RID: 37251 RVA: 0x001E35EA File Offset: 0x001E17EA
		public static string ToSource(Value value)
		{
			ToSourceVisitor toSourceVisitor = new ToSourceVisitor();
			toSourceVisitor.VisitValue(value);
			return toSourceVisitor.builder.ToString();
		}

		// Token: 0x06009184 RID: 37252 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override void VisitCycle(int depth, Value value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06009185 RID: 37253 RVA: 0x001E3604 File Offset: 0x001E1804
		protected override void VisitPrimitiveValue(Value value)
		{
			switch (value.Type.TypeKind)
			{
			case ValueKind.None:
			case ValueKind.Null:
			case ValueKind.Time:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			case ValueKind.Duration:
			case ValueKind.Number:
			case ValueKind.Logical:
			case ValueKind.Text:
				this.builder.Append(value.ToSource());
				return;
			case ValueKind.Binary:
				ToSourceVisitor.WriteBinary(this.builder, value.AsBinary);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06009186 RID: 37254 RVA: 0x001E3684 File Offset: 0x001E1884
		protected override void VisitRecord(RecordValue record)
		{
			this.builder.Append('[');
			for (int i = 0; i < record.Count; i++)
			{
				if (i > 0)
				{
					this.builder.Append(", ");
				}
				this.builder.Append(LanguageServices.FieldIdentifier.Escape(record.Keys[i]));
				this.builder.Append('=');
				this.VisitValue(record[i]);
			}
			this.builder.Append(']');
		}

		// Token: 0x06009187 RID: 37255 RVA: 0x001E370C File Offset: 0x001E190C
		protected override void VisitList(ListValue list)
		{
			this.builder.Append('{');
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					this.builder.Append(", ");
				}
				this.VisitValue(list[i]);
			}
			this.builder.Append('}');
		}

		// Token: 0x06009188 RID: 37256 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override void VisitTable(TableValue table)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06009189 RID: 37257 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override void VisitFunction(FunctionValue function)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600918A RID: 37258 RVA: 0x0000336E File Offset: 0x0000156E
		protected override void VisitMetaValue(RecordValue metaValue)
		{
		}

		// Token: 0x0600918B RID: 37259 RVA: 0x001E3768 File Offset: 0x001E1968
		protected override void VisitType(TypeValue type)
		{
			this.builder.Append(type.ToSource());
		}

		// Token: 0x0600918C RID: 37260 RVA: 0x001E377C File Offset: 0x001E197C
		private static void WriteBinary(StringBuilder builder, BinaryValue binary)
		{
			builder.Append("Binary.FromText(");
			using (Stream stream = binary.Open())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					builder.Append(Escape.AsQuotedString(Convert.ToBase64String(memoryStream.ToArray())));
				}
			}
			builder.Append(')');
		}

		// Token: 0x04004E1B RID: 19995
		private readonly StringBuilder builder = new StringBuilder();
	}
}
