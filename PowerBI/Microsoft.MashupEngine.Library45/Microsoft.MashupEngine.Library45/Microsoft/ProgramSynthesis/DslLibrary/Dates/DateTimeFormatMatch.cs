using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.FormatParsing;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000831 RID: 2097
	public class DateTimeFormatMatch : IEquatable<DateTimeFormatMatch>, IPartialParse<PartialDateTime, DateTimeFormat, StringRegion, DateTimeFormatMatch>
	{
		// Token: 0x06002D3F RID: 11583 RVA: 0x000804D3 File Offset: 0x0007E6D3
		public DateTimeFormatMatch(StringRegion region, DateTimeFormat dateTimeFormat, PartialDateTime partialDateTime)
		{
			this.Region = region;
			this.DateTimeFormat = dateTimeFormat;
			this.PartialDateTime = partialDateTime;
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000804F0 File Offset: 0x0007E6F0
		public PartialDateTime PartialDateTime { get; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000804F8 File Offset: 0x0007E6F8
		public DateTimeFormat DateTimeFormat { get; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x00080500 File Offset: 0x0007E700
		public StringRegion Region { get; }

		// Token: 0x06002D43 RID: 11587 RVA: 0x00080508 File Offset: 0x0007E708
		internal DateTimeFormatMatch ExpandWithConstant(StringRegion newRegion)
		{
			if (newRegion.Source != this.Region.Source || newRegion.Start > this.Region.Start || newRegion.End < this.Region.End)
			{
				throw new ArgumentException("New region must contain region.", "newRegion");
			}
			if (object.Equals(this.Region, newRegion))
			{
				return this;
			}
			List<DateTimeFormatPart> list = this.DateTimeFormat.FormatParts.ToList<DateTimeFormatPart>();
			if (newRegion.Start < this.Region.Start)
			{
				list.Insert(0, new ConstantDateTimeFormatPart(newRegion.Slice(newRegion.Start, this.Region.Start)));
			}
			if (newRegion.End > this.Region.End)
			{
				list.Insert(0, new ConstantDateTimeFormatPart(newRegion.Slice(this.Region.End, newRegion.End)));
			}
			DateTimeFormat dateTimeFormat = new DateTimeFormat(list);
			return new DateTimeFormatMatch(newRegion, dateTimeFormat, this.PartialDateTime);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000805FF File Offset: 0x0007E7FF
		public bool Equals(DateTimeFormatMatch other)
		{
			return other != null && (this == other || (this.DateTimeFormat.Equals(other.DateTimeFormat) && this.Region.Equals(other.Region)));
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00080632 File Offset: 0x0007E832
		internal bool Explains(DateTimeFormatMatch other)
		{
			return this.PartialDateTime.Explains(other.PartialDateTime);
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00080645 File Offset: 0x0007E845
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[match={0}, format={1}, partialDateTime={2}]", new object[] { this.Region, this.DateTimeFormat, this.PartialDateTime }));
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00080677 File Offset: 0x0007E877
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((DateTimeFormatMatch)obj)));
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000806A5 File Offset: 0x0007E8A5
		public override int GetHashCode()
		{
			return (this.DateTimeFormat.GetHashCode() * 11999179) ^ this.Region.GetHashCode();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(DateTimeFormatMatch left, DateTimeFormatMatch right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(DateTimeFormatMatch left, DateTimeFormatMatch right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000806C4 File Offset: 0x0007E8C4
		public StringRegion ParsedRegion
		{
			get
			{
				return this.Region;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000806CC File Offset: 0x0007E8CC
		public PartialDateTime CompleteParse
		{
			get
			{
				return this.PartialDateTime;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000806D4 File Offset: 0x0007E8D4
		public bool ContainsOnlyEmptyParse
		{
			get
			{
				return this.DateTimeFormat.FormatParts.Count == 1 && this.DateTimeFormat.FormatParts.Single<DateTimeFormatPart>() is EmptyDateTimeFormatPart;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x00080703 File Offset: 0x0007E903
		public Optional<DateTimeFormatMatch> Sequence(DateTimeFormatMatch other)
		{
			return this.Sequence(other).SomeIfNotNull<DateTimeFormatMatch>();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x00080711 File Offset: 0x0007E911
		public static DateTimeFormatMatch Empty(StringRegion region)
		{
			return new DateTimeFormatMatch(region.Slice(region.Start, region.Start), DateTimeFormat.Empty, PartialDateTime.Empty);
		}
	}
}
