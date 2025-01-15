using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000271 RID: 625
	[DataContract(Name = "Annotations", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class FilterAnnotations : IEquatable<FilterAnnotations>
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000222B6 File Offset: 0x000204B6
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x000222BE File Offset: 0x000204BE
		[DataMember(Name = "PowerBI.MParameterBehavior", IsRequired = false, Order = 1, EmitDefaultValue = false)]
		public MParameterBehavior MParameterBehavior { get; set; }

		// Token: 0x06001309 RID: 4873 RVA: 0x000222C7 File Offset: 0x000204C7
		public FilterAnnotations(MParameterBehavior mParameterBehavior)
		{
			this.MParameterBehavior = mParameterBehavior;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000222D8 File Offset: 0x000204D8
		public bool Equals(FilterAnnotations other)
		{
			bool? flag = Util.AreEqual<FilterAnnotations>(this, other);
			return flag != null && flag != null && flag.Value;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00022308 File Offset: 0x00020508
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FilterAnnotations);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00022316 File Offset: 0x00020516
		public override int GetHashCode()
		{
			return Hashing.GetHashCode<MParameterBehavior>(this.MParameterBehavior, null);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00022324 File Offset: 0x00020524
		internal void WriteQueryString(QueryStringWriter w)
		{
			try
			{
				w.Write(string.Format("PowerBI.MParameterBehavior={0}", this.MParameterBehavior));
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
