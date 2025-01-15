using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016CA RID: 5834
	public static class VisitorExtensions
	{
		// Token: 0x06009476 RID: 38006 RVA: 0x001EA031 File Offset: 0x001E8231
		public static string CreateCacheKey(this Value record)
		{
			return new VisitorExtensions.CacheKeyVisitor().CalculateKey(record);
		}

		// Token: 0x06009477 RID: 38007 RVA: 0x001EA03E File Offset: 0x001E823E
		public static string PrimitiveAndRecordToString(this Value value, int maximumDepth)
		{
			return new VisitorExtensions.PrimitiveAndRecordToStringVisitor(maximumDepth).ToString(value);
		}

		// Token: 0x020016CB RID: 5835
		private class CacheKeyVisitor : ValueVisitor
		{
			// Token: 0x06009478 RID: 38008 RVA: 0x001EA04C File Offset: 0x001E824C
			public string CalculateKey(Value value)
			{
				string text;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (this.writer = new BinaryWriter(memoryStream))
					{
						this.failed = false;
						this.VisitValue(value);
						if (this.failed)
						{
							text = null;
						}
						else
						{
							memoryStream.Position = 0L;
							using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
							{
								text = Convert.ToBase64String(hashAlgorithm.ComputeHash(memoryStream));
							}
						}
					}
				}
				return text;
			}

			// Token: 0x06009479 RID: 38009 RVA: 0x001EA0F4 File Offset: 0x001E82F4
			public override void VisitValue(Value value)
			{
				if (!this.failed)
				{
					base.VisitValue(value);
				}
			}

			// Token: 0x0600947A RID: 38010 RVA: 0x001EA105 File Offset: 0x001E8305
			protected override void VisitCycle(int depth, Value value)
			{
				this.failed = true;
			}

			// Token: 0x0600947B RID: 38011 RVA: 0x001EA110 File Offset: 0x001E8310
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
					this.writer.Write(value.ToSource());
					return;
				case ValueKind.Binary:
					this.writer.Write(value.AsBinary.AsBytes);
					return;
				}
				this.failed = true;
			}

			// Token: 0x0600947C RID: 38012 RVA: 0x001EA194 File Offset: 0x001E8394
			protected override void VisitList(ListValue list)
			{
				if (list.IsBuffered)
				{
					for (int i = 0; i < list.Count; i++)
					{
						this.writer.Write(i);
						this.VisitValue(list[i]);
					}
					return;
				}
				this.failed = true;
			}

			// Token: 0x0600947D RID: 38013 RVA: 0x001EA105 File Offset: 0x001E8305
			protected override void VisitTable(TableValue table)
			{
				this.failed = true;
			}

			// Token: 0x0600947E RID: 38014 RVA: 0x001EA1DC File Offset: 0x001E83DC
			protected override void VisitRecord(RecordValue record)
			{
				foreach (NamedValue namedValue in from s in record.GetFields()
					orderby s.Key
					select s)
				{
					this.writer.Write(namedValue.Key);
					this.VisitValue(namedValue.Value);
				}
			}

			// Token: 0x04004EF7 RID: 20215
			private BinaryWriter writer;

			// Token: 0x04004EF8 RID: 20216
			private bool failed;
		}

		// Token: 0x020016CD RID: 5837
		private class PrimitiveAndRecordToStringVisitor : ValueVisitor
		{
			// Token: 0x06009483 RID: 38019 RVA: 0x001EA278 File Offset: 0x001E8478
			public PrimitiveAndRecordToStringVisitor(int maximumDepth)
			{
				this.maximumDepth = maximumDepth;
			}

			// Token: 0x06009484 RID: 38020 RVA: 0x001EA287 File Offset: 0x001E8487
			public override void VisitValue(Value value)
			{
				base.VisitNonMetaValue(value);
				this.VisitMetaValue(value.MetaValue);
			}

			// Token: 0x06009485 RID: 38021 RVA: 0x001EA29C File Offset: 0x001E849C
			public string ToString(Value value)
			{
				this.builder = new StringBuilder();
				this.VisitValue(value);
				return this.builder.ToString();
			}

			// Token: 0x06009486 RID: 38022 RVA: 0x001EA2BB File Offset: 0x001E84BB
			protected override void VisitAction(ActionValue action)
			{
				this.builder.Append("Action.Return(...)");
			}

			// Token: 0x06009487 RID: 38023 RVA: 0x001EA2CE File Offset: 0x001E84CE
			protected override void VisitCycle(int depth, Value value)
			{
				this.builder.Append("...");
			}

			// Token: 0x06009488 RID: 38024 RVA: 0x001EA2E1 File Offset: 0x001E84E1
			protected override void VisitFunction(FunctionValue function)
			{
				this.builder.Append("(...) => ...");
			}

			// Token: 0x06009489 RID: 38025 RVA: 0x001EA2F4 File Offset: 0x001E84F4
			protected override void VisitList(ListValue list)
			{
				this.builder.Append("{...}");
			}

			// Token: 0x0600948A RID: 38026 RVA: 0x001EA307 File Offset: 0x001E8507
			protected override void VisitMetaValue(RecordValue metaValue)
			{
				if (metaValue.Keys.Any<string>())
				{
					this.builder.Append(" meta ");
					base.VisitMetaValue(metaValue);
				}
			}

			// Token: 0x0600948B RID: 38027 RVA: 0x001EA32E File Offset: 0x001E852E
			protected override void VisitPrimitiveValue(Value value)
			{
				this.builder.Append(value.ToSource());
			}

			// Token: 0x0600948C RID: 38028 RVA: 0x001EA344 File Offset: 0x001E8544
			protected override void VisitRecord(RecordValue record)
			{
				if (this.maximumDepth >= 0 && this.maximumDepth <= base.Depth)
				{
					this.builder.Append("[...]");
					return;
				}
				this.builder.Append("[");
				RecordValue fields = record.Type.AsRecordType.Fields;
				int i = 0;
				while (i < record.Count)
				{
					if (i != 0)
					{
						this.builder.Append(", ");
					}
					this.builder.Append(record.Keys[i]);
					this.builder.Append(" = ");
					string text;
					if (!PreviewServices.IsDelayed(fields[i]["Type"].AsType, out text))
					{
						try
						{
							this.VisitValue(record[i]);
							goto IL_00E2;
						}
						catch (ValueException)
						{
							this.builder.Append("error ...");
							goto IL_00E2;
						}
						goto IL_00D1;
					}
					goto IL_00D1;
					IL_00E2:
					i++;
					continue;
					IL_00D1:
					this.builder.Append("...");
					goto IL_00E2;
				}
				this.builder.Append("]");
			}

			// Token: 0x0600948D RID: 38029 RVA: 0x000091AE File Offset: 0x000073AE
			protected override void VisitRecordField(string name, Value value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600948E RID: 38030 RVA: 0x001EA464 File Offset: 0x001E8664
			protected override void VisitTable(TableValue table)
			{
				this.builder.Append("#table({...}, {...})");
			}

			// Token: 0x0600948F RID: 38031 RVA: 0x001EA477 File Offset: 0x001E8677
			protected override void VisitType(TypeValue type)
			{
				this.builder.Append("type ...");
			}

			// Token: 0x04004EFB RID: 20219
			private int maximumDepth;

			// Token: 0x04004EFC RID: 20220
			private StringBuilder builder;
		}
	}
}
