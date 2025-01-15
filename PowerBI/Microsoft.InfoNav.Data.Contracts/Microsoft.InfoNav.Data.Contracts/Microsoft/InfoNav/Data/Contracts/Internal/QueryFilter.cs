using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B5 RID: 693
	[DataContract(Name = "Filter", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryFilter : IEquatable<QueryFilter>
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00029224 File Offset: 0x00027424
		// (set) Token: 0x0600170D RID: 5901 RVA: 0x0002922C File Offset: 0x0002742C
		[DataMember(IsRequired = false, Order = 1, EmitDefaultValue = false)]
		public List<QueryExpressionContainer> Target { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00029235 File Offset: 0x00027435
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x0002923D File Offset: 0x0002743D
		[DataMember(IsRequired = true, Order = 2)]
		public QueryExpressionContainer Condition { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00029246 File Offset: 0x00027446
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x0002924E File Offset: 0x0002744E
		[DataMember(IsRequired = false, Order = 3, EmitDefaultValue = false)]
		public FilterAnnotations Annotations { get; set; }

		// Token: 0x06001712 RID: 5906 RVA: 0x00029258 File Offset: 0x00027458
		public bool Equals(QueryFilter other)
		{
			bool? flag = Util.AreEqual<QueryFilter>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Target.SequenceEqual(other.Target) && object.Equals(this.Condition, other.Condition) && object.Equals(this.Annotations, other.Annotations);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000292B7 File Offset: 0x000274B7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryFilter);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000292C5 File Offset: 0x000274C5
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<QueryExpressionContainer>(this.Target, null), Hashing.GetHashCode<QueryExpressionContainer>(this.Condition, null), Hashing.GetHashCode<FilterAnnotations>(this.Annotations, null));
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000292F0 File Offset: 0x000274F0
		public static bool operator ==(QueryFilter left, QueryFilter right)
		{
			bool? flag = Util.AreEqual<QueryFilter>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0002931D File Offset: 0x0002751D
		public static bool operator !=(QueryFilter left, QueryFilter right)
		{
			return !(left == right);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0002932C File Offset: 0x0002752C
		internal void WriteQueryString(QueryStringWriter w, string filterRestatement = null)
		{
			try
			{
				if (this.Target != null && this.Target.Count > 0)
				{
					if (this.Target.Count == 1)
					{
						this.Target[0].WriteQueryString(w);
					}
					else
					{
						w.Write('(');
						using (w.NewSeparatorScope(QueryStringWriter.Separator.Comma))
						{
							foreach (QueryExpressionContainer queryExpressionContainer in this.Target)
							{
								w.WriteSeparator();
								queryExpressionContainer.WriteQueryString(w);
							}
						}
						w.Write(')');
					}
					w.Write(" has ");
				}
				this.Condition.WriteQueryString(w);
				if (this.Annotations != null)
				{
					w.Write(" annotations [");
					this.Annotations.WriteQueryString(w);
					w.Write("]");
				}
				if (filterRestatement != null)
				{
					w.Write(" /* ");
					w.WriteCustomerContent(filterRestatement);
					w.Write(" */");
				}
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (w.TraceString)
				{
					w.Write(ex.ToString());
				}
				throw;
			}
		}
	}
}
