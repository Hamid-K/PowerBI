using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates
{
	// Token: 0x02001D86 RID: 7558
	public class FormattedPartialDateTimeType : IType<PartialDateTime>, IType, IEquatable<IType>, IEquatable<FormattedPartialDateTimeType>
	{
		// Token: 0x0600FE0F RID: 65039 RVA: 0x0036454A File Offset: 0x0036274A
		public FormattedPartialDateTimeType(DateTimeFormat format)
		{
			this.Format = format;
		}

		// Token: 0x17002A54 RID: 10836
		// (get) Token: 0x0600FE10 RID: 65040 RVA: 0x00364559 File Offset: 0x00362759
		public DateTimeFormat Format { get; }

		// Token: 0x0600FE11 RID: 65041 RVA: 0x00364561 File Offset: 0x00362761
		public bool Equals(FormattedPartialDateTimeType other)
		{
			return other != null && (this == other || this.Format.Equals(other.Format));
		}

		// Token: 0x0600FE12 RID: 65042 RVA: 0x0036457F File Offset: 0x0036277F
		public bool IsAssignableFrom(IType other)
		{
			FormattedPartialDateTimeType formattedPartialDateTimeType = other as FormattedPartialDateTimeType;
			return formattedPartialDateTimeType != null && formattedPartialDateTimeType.Format.Equals(this.Format);
		}

		// Token: 0x0600FE13 RID: 65043 RVA: 0x003645A0 File Offset: 0x003627A0
		public bool IsValidObject(ITypedValue obj)
		{
			string value = obj.Value;
			return value != null && this.Format.Parse(value).HasValue;
		}

		// Token: 0x0600FE14 RID: 65044 RVA: 0x003645D0 File Offset: 0x003627D0
		Optional<PartialDateTime> IType<PartialDateTime>.GetTypedValue(ITypedValue obj)
		{
			return from match in obj.Value.SomeIfNotNull<string>().SelectMany((string str) => this.Format.Parse(str))
				select match.PartialDateTime;
		}

		// Token: 0x0600FE15 RID: 65045 RVA: 0x00024CEC File Offset: 0x00022EEC
		public bool Equals(IType other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600FE16 RID: 65046 RVA: 0x0036461D File Offset: 0x0036281D
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("FormattedPartialDateTimeType(Format={0})", new object[] { this.Format }));
		}

		// Token: 0x0600FE17 RID: 65047 RVA: 0x0036463D File Offset: 0x0036283D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FormattedPartialDateTimeType)obj)));
		}

		// Token: 0x0600FE18 RID: 65048 RVA: 0x0036466B File Offset: 0x0036286B
		public override int GetHashCode()
		{
			return this.Format.GetHashCode();
		}
	}
}
