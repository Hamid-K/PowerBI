using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates
{
	// Token: 0x02001D85 RID: 7557
	public class PartialDateTimeType : IType<PartialDateTime>, IType, IEquatable<IType>, IEquatable<PartialDateTimeType>
	{
		// Token: 0x17002A50 RID: 10832
		// (get) Token: 0x0600FE01 RID: 65025 RVA: 0x0036438A File Offset: 0x0036258A
		public static PartialDateTimeType Any { get; } = new PartialDateTimeType(null);

		// Token: 0x17002A51 RID: 10833
		// (get) Token: 0x0600FE02 RID: 65026 RVA: 0x00364391 File Offset: 0x00362591
		public static PartialDateTimeType FullDate { get; } = new PartialDateTimeType(new DateTimePart[]
		{
			DateTimePart.Year,
			DateTimePart.Month,
			DateTimePart.Day
		});

		// Token: 0x0600FE03 RID: 65027 RVA: 0x00364398 File Offset: 0x00362598
		[JsonConstructor]
		public PartialDateTimeType(IEnumerable<DateTimePart> parts = null)
		{
			this.Parts = ((parts == null) ? DateTimePartSet.Empty : new DateTimePartSet(parts));
		}

		// Token: 0x17002A52 RID: 10834
		// (get) Token: 0x0600FE04 RID: 65028 RVA: 0x003643B6 File Offset: 0x003625B6
		[JsonIgnore]
		public DateTimePartSet Parts { get; }

		// Token: 0x17002A53 RID: 10835
		// (get) Token: 0x0600FE05 RID: 65029 RVA: 0x003643C0 File Offset: 0x003625C0
		[JsonProperty("Parts")]
		private IEnumerable<DateTimePart> JsonParts
		{
			get
			{
				return this.Parts.AsEnumerable();
			}
		}

		// Token: 0x0600FE06 RID: 65030 RVA: 0x003643DB File Offset: 0x003625DB
		public bool Equals(PartialDateTimeType other)
		{
			return other != null && (this == other || this.Parts == other.Parts);
		}

		// Token: 0x0600FE07 RID: 65031 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsValidObject(ITypedValue obj)
		{
			return false;
		}

		// Token: 0x0600FE08 RID: 65032 RVA: 0x003643FC File Offset: 0x003625FC
		public bool IsAssignableFrom(IType other)
		{
			if (!(other is IType<PartialDateTime>))
			{
				return false;
			}
			if (!this.Parts.Any())
			{
				return true;
			}
			PartialDateTimeType partialDateTimeType = other as PartialDateTimeType;
			DateTimePartSet? dateTimePartSet;
			if (partialDateTimeType == null)
			{
				FormattedPartialDateTimeType formattedPartialDateTimeType = other as FormattedPartialDateTimeType;
				dateTimePartSet = ((formattedPartialDateTimeType != null) ? new DateTimePartSet?(formattedPartialDateTimeType.Format.MatchedParts) : null);
			}
			else
			{
				dateTimePartSet = new DateTimePartSet?(partialDateTimeType.Parts);
			}
			DateTimePartSet? dateTimePartSet2 = dateTimePartSet;
			return dateTimePartSet2 != null && dateTimePartSet2.GetValueOrDefault().Contains(this.Parts);
		}

		// Token: 0x0600FE09 RID: 65033 RVA: 0x00364480 File Offset: 0x00362680
		Optional<PartialDateTime> IType<PartialDateTime>.GetTypedValue(ITypedValue obj)
		{
			return Optional<PartialDateTime>.Nothing;
		}

		// Token: 0x0600FE0A RID: 65034 RVA: 0x00024CEC File Offset: 0x00022EEC
		public bool Equals(IType other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600FE0B RID: 65035 RVA: 0x00364487 File Offset: 0x00362687
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PartialDateTimeType)obj)));
		}

		// Token: 0x0600FE0C RID: 65036 RVA: 0x003644B8 File Offset: 0x003626B8
		public override int GetHashCode()
		{
			return 435221 ^ this.Parts.GetHashCode();
		}

		// Token: 0x0600FE0D RID: 65037 RVA: 0x003644E0 File Offset: 0x003626E0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}(Parts={{{1}}})", new object[]
			{
				"PartialDateTimeType",
				string.Join<DateTimePart>(", ", this.Parts.AsEnumerable())
			}));
		}
	}
}
