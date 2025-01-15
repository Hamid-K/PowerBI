using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200157D RID: 5501
	public abstract class LogicalValue : PrimitiveValue
	{
		// Token: 0x060088F7 RID: 35063 RVA: 0x001D01B9 File Offset: 0x001CE3B9
		public static LogicalValue New(bool value)
		{
			if (!value)
			{
				return LogicalValue.False;
			}
			return LogicalValue.True;
		}

		// Token: 0x060088F8 RID: 35064 RVA: 0x001D01C9 File Offset: 0x001CE3C9
		public static LogicalValue New(bool value, RecordValue meta, TypeValue type)
		{
			if (meta.AsRecord.IsEmpty && type == TypeValue.Logical)
			{
				return LogicalValue.New(value);
			}
			if (value)
			{
				return new LogicalValue.TrueValue(meta, type);
			}
			return new LogicalValue.FalseValue(meta, type);
		}

		// Token: 0x060088F9 RID: 35065 RVA: 0x001D01FC File Offset: 0x001CE3FC
		public static bool TryParseFromText(string text, out LogicalValue logicalValue)
		{
			bool flag;
			if (bool.TryParse(text, out flag))
			{
				logicalValue = LogicalValue.New(flag);
				return true;
			}
			logicalValue = null;
			return false;
		}

		// Token: 0x060088FA RID: 35066 RVA: 0x001D0221 File Offset: 0x001CE421
		protected LogicalValue(bool value, RecordValue meta, TypeValue type)
		{
			this.value = value;
			this.meta = meta;
			this.type = type;
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x060088FB RID: 35067 RVA: 0x001D023E File Offset: 0x001CE43E
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060088FC RID: 35068 RVA: 0x001D0246 File Offset: 0x001CE446
		public override Value NewType(TypeValue type)
		{
			return LogicalValue.New(this.value, this.meta, type);
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x060088FD RID: 35069 RVA: 0x00002475 File Offset: 0x00000675
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Logical;
			}
		}

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x060088FE RID: 35070 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsLogical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x060088FF RID: 35071 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override LogicalValue AsLogical
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x06008900 RID: 35072 RVA: 0x001D025A File Offset: 0x001CE45A
		public bool Boolean
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06008901 RID: 35073 RVA: 0x001D0262 File Offset: 0x001CE462
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsLogical && value.AsBoolean == this.value;
		}

		// Token: 0x06008902 RID: 35074 RVA: 0x001D027C File Offset: 0x001CE47C
		public override int GetHashCode(_ValueComparer comparer)
		{
			if (!this.value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06008903 RID: 35075 RVA: 0x001D028C File Offset: 0x001CE48C
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsLogical)
			{
				return this.value.CompareTo(value.AsBoolean);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06008904 RID: 35076 RVA: 0x001D02BE File Offset: 0x001CE4BE
		public override RecordValue MetaValue
		{
			get
			{
				return this.meta;
			}
		}

		// Token: 0x06008905 RID: 35077 RVA: 0x001D02C6 File Offset: 0x001CE4C6
		public override Value NewMeta(RecordValue metaValue)
		{
			return LogicalValue.New(this.value, metaValue, this.type);
		}

		// Token: 0x04004BB9 RID: 19385
		public static readonly LogicalValue True = new LogicalValue.TrueValue(RecordValue.Empty, TypeValue.Logical);

		// Token: 0x04004BBA RID: 19386
		public static readonly LogicalValue False = new LogicalValue.FalseValue(RecordValue.Empty, TypeValue.Logical);

		// Token: 0x04004BBB RID: 19387
		private readonly bool value;

		// Token: 0x04004BBC RID: 19388
		private readonly RecordValue meta;

		// Token: 0x04004BBD RID: 19389
		private readonly TypeValue type;

		// Token: 0x0200157E RID: 5502
		private class TrueValue : LogicalValue
		{
			// Token: 0x06008907 RID: 35079 RVA: 0x001D0304 File Offset: 0x001CE504
			public TrueValue(RecordValue meta, TypeValue type)
				: base(true, meta, type)
			{
			}

			// Token: 0x06008908 RID: 35080 RVA: 0x0001995F File Offset: 0x00017B5F
			public sealed override string ToSource()
			{
				return "true";
			}

			// Token: 0x06008909 RID: 35081 RVA: 0x0001995F File Offset: 0x00017B5F
			public sealed override string ToString()
			{
				return "true";
			}

			// Token: 0x0600890A RID: 35082 RVA: 0x001D030F File Offset: 0x001CE50F
			public sealed override object ToOleDb(Type type)
			{
				if (type == typeof(bool) || type == typeof(object))
				{
					return true;
				}
				throw ValueMarshaller.CreateTypeError(this, type);
			}

			// Token: 0x0600890B RID: 35083 RVA: 0x000982FC File Offset: 0x000964FC
			public override Value Not()
			{
				return LogicalValue.False;
			}
		}

		// Token: 0x0200157F RID: 5503
		private class FalseValue : LogicalValue
		{
			// Token: 0x0600890C RID: 35084 RVA: 0x001D0343 File Offset: 0x001CE543
			public FalseValue(RecordValue meta, TypeValue type)
				: base(false, meta, type)
			{
			}

			// Token: 0x0600890D RID: 35085 RVA: 0x00019966 File Offset: 0x00017B66
			public sealed override string ToSource()
			{
				return "false";
			}

			// Token: 0x0600890E RID: 35086 RVA: 0x00019966 File Offset: 0x00017B66
			public sealed override string ToString()
			{
				return "false";
			}

			// Token: 0x0600890F RID: 35087 RVA: 0x001D034E File Offset: 0x001CE54E
			public sealed override object ToOleDb(Type type)
			{
				if (type == typeof(bool) || type == typeof(object))
				{
					return false;
				}
				throw ValueMarshaller.CreateTypeError(this, type);
			}

			// Token: 0x06008910 RID: 35088 RVA: 0x001D0382 File Offset: 0x001CE582
			public override Value Not()
			{
				return LogicalValue.True;
			}
		}
	}
}
