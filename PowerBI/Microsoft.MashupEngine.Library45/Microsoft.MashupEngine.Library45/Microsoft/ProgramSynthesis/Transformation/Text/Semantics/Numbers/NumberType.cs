using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers
{
	// Token: 0x02001D6D RID: 7533
	public class NumberType : IType<decimal>, IType, IEquatable<IType>, IEquatable<NumberType>
	{
		// Token: 0x0600FD73 RID: 64883 RVA: 0x0036269B File Offset: 0x0036089B
		public NumberType(NumberFormat format = null)
		{
			this.Format = format;
		}

		// Token: 0x17002A36 RID: 10806
		// (get) Token: 0x0600FD74 RID: 64884 RVA: 0x003626AA File Offset: 0x003608AA
		public NumberFormat Format { get; }

		// Token: 0x0600FD75 RID: 64885 RVA: 0x003626B2 File Offset: 0x003608B2
		public bool Equals(NumberType other)
		{
			return other != null && (this == other || object.Equals(this.Format, other.Format));
		}

		// Token: 0x0600FD76 RID: 64886 RVA: 0x003626D0 File Offset: 0x003608D0
		public Optional<decimal> GetTypedValue(ITypedValue obj)
		{
			ValueSubstring valueSubstring = ValueSubstring.Create(obj.Value, null, null, null, null);
			NumberFormat format = this.Format;
			return Semantics.ParseNumber(valueSubstring, ((format != null) ? format.Details : null) ?? NumberFormatDetails.Default).SomeIfNotNull<decimal>();
		}

		// Token: 0x0600FD77 RID: 64887 RVA: 0x00362724 File Offset: 0x00360924
		public bool IsValidObject(ITypedValue obj)
		{
			Optional<decimal> typedValue = this.GetTypedValue(obj);
			if (this.Format == null)
			{
				return typedValue.HasValue;
			}
			return this.Format.ToString(typedValue.OrElseDefault<decimal>()) == obj.Value;
		}

		// Token: 0x0600FD78 RID: 64888 RVA: 0x0036276B File Offset: 0x0036096B
		public bool IsAssignableFrom(IType other)
		{
			if (this.Format == null)
			{
				return other is IType<decimal>;
			}
			NumberFormat format = this.Format;
			NumberType numberType = other as NumberType;
			return format.Equals((numberType != null) ? numberType.Format : null);
		}

		// Token: 0x0600FD79 RID: 64889 RVA: 0x00024CEC File Offset: 0x00022EEC
		public bool Equals(IType other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600FD7A RID: 64890 RVA: 0x003627A2 File Offset: 0x003609A2
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((NumberType)obj)));
		}

		// Token: 0x0600FD7B RID: 64891 RVA: 0x003627D0 File Offset: 0x003609D0
		public override int GetHashCode()
		{
			NumberFormat format = this.Format;
			if (format == null)
			{
				return 0;
			}
			return format.GetHashCode();
		}

		// Token: 0x0600FD7C RID: 64892 RVA: 0x003627E3 File Offset: 0x003609E3
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("NumberType(Format={0})", new object[] { this.Format }));
		}

		// Token: 0x0600FD7D RID: 64893 RVA: 0x00362804 File Offset: 0x00360A04
		// Note: this type is marked as 'beforefieldinit'.
		static NumberType()
		{
			Optional<uint> optional = 0U.Some<uint>();
			NumberType.Integer = new NumberType(new NumberFormat(default(Optional<uint>), optional, default(Optional<uint>), default(Optional<uint>), default(Optional<uint>), null));
		}

		// Token: 0x04005EB9 RID: 24249
		public static NumberType Integer;
	}
}
