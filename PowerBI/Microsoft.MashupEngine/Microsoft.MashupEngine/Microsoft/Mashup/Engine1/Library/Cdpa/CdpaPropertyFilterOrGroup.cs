using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DDF RID: 3551
	[DataContract]
	internal class CdpaPropertyFilterOrGroup : IEquatable<CdpaPropertyFilterOrGroup>, IIntersectable<CdpaPropertyFilterOrGroup>, IUnionable<CdpaPropertyFilterOrGroup>
	{
		// Token: 0x06005FF7 RID: 24567 RVA: 0x00148F63 File Offset: 0x00147163
		public CdpaPropertyFilterOrGroup()
		{
			this.PropertyFilterAndGroup = EmptyArray<CdpaPropertyFilterAndGroup>.Instance;
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x00148F76 File Offset: 0x00147176
		// (set) Token: 0x06005FF9 RID: 24569 RVA: 0x00148F7E File Offset: 0x0014717E
		[DataMember(Name = "propertyFilterAndGroup", IsRequired = true)]
		public IList<CdpaPropertyFilterAndGroup> PropertyFilterAndGroup { get; set; }

		// Token: 0x06005FFA RID: 24570 RVA: 0x00148F88 File Offset: 0x00147188
		public CdpaPropertyFilterOrGroup And(CdpaPropertyFilterOrGroup other)
		{
			HashSet<CdpaPropertyFilterAndGroup> hashSet = new HashSet<CdpaPropertyFilterAndGroup>();
			foreach (CdpaPropertyFilterAndGroup cdpaPropertyFilterAndGroup in this.PropertyFilterAndGroup)
			{
				foreach (CdpaPropertyFilterAndGroup cdpaPropertyFilterAndGroup2 in other.PropertyFilterAndGroup)
				{
					hashSet.Add(cdpaPropertyFilterAndGroup.And(cdpaPropertyFilterAndGroup2));
				}
			}
			return new CdpaPropertyFilterOrGroup
			{
				PropertyFilterAndGroup = hashSet.ToArray<CdpaPropertyFilterAndGroup>()
			};
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0014902C File Offset: 0x0014722C
		public CdpaPropertyFilterOrGroup Or(CdpaPropertyFilterOrGroup other)
		{
			CdpaPropertyFilterAndGroup[] array = this.PropertyFilterAndGroup.Union(other.PropertyFilterAndGroup).ToArray<CdpaPropertyFilterAndGroup>();
			string text;
			string text2;
			CdpaListValue cdpaListValue;
			if (CdpaPropertyFilterOrGroup.TryGetMemberValues(array, out text, out text2, out cdpaListValue) && text2 == "equals")
			{
				return new IsOneOfMembershipCdpaPropertyFilter
				{
					PropertyName = text,
					Values = cdpaListValue
				}.ToAndGroup().ToOrGroup();
			}
			return new CdpaPropertyFilterOrGroup
			{
				PropertyFilterAndGroup = array
			};
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x00149095 File Offset: 0x00147295
		public CdpaPropertyFilterOrGroup Intersect(CdpaPropertyFilterOrGroup other)
		{
			return this.And(other);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x0014909E File Offset: 0x0014729E
		public CdpaPropertyFilterOrGroup Union(CdpaPropertyFilterOrGroup other)
		{
			return this.Or(other);
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x001490A7 File Offset: 0x001472A7
		public CdpaPropertyFilterOrGroup Not()
		{
			if (this.PropertyFilterAndGroup.Count == 1)
			{
				return this.PropertyFilterAndGroup[0].Not();
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x001490CE File Offset: 0x001472CE
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaPropertyFilterOrGroup);
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001490DC File Offset: 0x001472DC
		public bool Equals(CdpaPropertyFilterOrGroup other)
		{
			return other != null && this.PropertyFilterAndGroup.SetEquals(other.PropertyFilterAndGroup);
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x001490F4 File Offset: 0x001472F4
		public override int GetHashCode()
		{
			return this.PropertyFilterAndGroup.SetGetHashCode<CdpaPropertyFilterAndGroup>();
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x00149104 File Offset: 0x00147304
		private static bool TryGetMemberValues(CdpaPropertyFilterAndGroup[] andGroup, out string propertyName, out string op, out CdpaListValue newValues)
		{
			string text = null;
			propertyName = null;
			op = null;
			newValues = new CdpaListValue
			{
				Type = null,
				Value = new List<object>(andGroup.Length)
			};
			int i = 0;
			while (i < andGroup.Length)
			{
				if (andGroup[i].PropertyFilters.Count == 1)
				{
					CdpaPropertyFilter cdpaPropertyFilter = andGroup[i].PropertyFilters[0];
					StringComparisonCdpaPropertyFilter stringComparisonCdpaPropertyFilter = cdpaPropertyFilter as StringComparisonCdpaPropertyFilter;
					if (stringComparisonCdpaPropertyFilter == null || stringComparisonCdpaPropertyFilter.ComparisonOptions != null)
					{
						goto IL_00B0;
					}
					propertyName = CdpaPropertyFilterOrGroup.NullableMergeOrNull(propertyName, stringComparisonCdpaPropertyFilter.PropertyName);
					text = CdpaPropertyFilterOrGroup.NullableMergeOrNull(text, "string");
					op = CdpaPropertyFilterOrGroup.NullableMergeOrNull(op, stringComparisonCdpaPropertyFilter.Operator);
					if (propertyName == null || text == null || op == null)
					{
						goto IL_00B0;
					}
					newValues.Value.Add(stringComparisonCdpaPropertyFilter.Value.Value);
					IL_01AF:
					i++;
					continue;
					IL_00B0:
					ValueComparisonCdpaPropertyFilter valueComparisonCdpaPropertyFilter = cdpaPropertyFilter as ValueComparisonCdpaPropertyFilter;
					if (valueComparisonCdpaPropertyFilter != null)
					{
						propertyName = CdpaPropertyFilterOrGroup.NullableMergeOrNull(propertyName, valueComparisonCdpaPropertyFilter.PropertyName);
						text = CdpaPropertyFilterOrGroup.NullableMergeOrNull(text, valueComparisonCdpaPropertyFilter.Value.Type);
						op = CdpaPropertyFilterOrGroup.NullableMergeOrNull(op, valueComparisonCdpaPropertyFilter.Operator);
						if (propertyName != null && text != null && op != null)
						{
							newValues.Value.Add(valueComparisonCdpaPropertyFilter.Value.Value);
							goto IL_01AF;
						}
					}
					IsOneOfMembershipCdpaPropertyFilter isOneOfMembershipCdpaPropertyFilter = cdpaPropertyFilter as IsOneOfMembershipCdpaPropertyFilter;
					if (isOneOfMembershipCdpaPropertyFilter != null)
					{
						propertyName = CdpaPropertyFilterOrGroup.NullableMergeOrNull(propertyName, isOneOfMembershipCdpaPropertyFilter.PropertyName);
						text = CdpaPropertyFilterOrGroup.NullableMergeOrNull(text, CdpaListType.ToItemType(isOneOfMembershipCdpaPropertyFilter.Values.Type));
						op = CdpaPropertyFilterOrGroup.NullableMergeOrNull(op, "equals");
						if (propertyName != null && text != null && op != null)
						{
							using (IEnumerator<object> enumerator = isOneOfMembershipCdpaPropertyFilter.Values.Value.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									newValues.Value.Add(obj);
								}
								goto IL_01AF;
							}
						}
					}
				}
				newValues = null;
				break;
			}
			if (newValues != null)
			{
				newValues.Type = CdpaListType.FromItemType(text);
			}
			return newValues != null;
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x001492F4 File Offset: 0x001474F4
		private static string NullableMergeOrNull(string s1, string s2)
		{
			if (s1 == null)
			{
				return s2;
			}
			if (s2 == null)
			{
				return s1;
			}
			if (s1.Equals(s2))
			{
				return s1;
			}
			return null;
		}
	}
}
