using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004F5 RID: 1269
	internal class SapBwVariableHierarchyNodeMemberProvider : SapBwVariableHierarchyNodeProvider
	{
		// Token: 0x0600296E RID: 10606 RVA: 0x0007BBA3 File Offset: 0x00079DA3
		public SapBwVariableHierarchyNodeMemberProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x0007BBB0 File Offset: 0x00079DB0
		public override bool HasValues
		{
			get
			{
				this.EnsureInitialized();
				SapBwMemberProvider sapBwMemberProvider = this.valueProviders.FirstOrDefault<SapBwMemberProvider>();
				return sapBwMemberProvider != null && sapBwMemberProvider.HasValues;
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0007BBDA File Offset: 0x00079DDA
		public override IEnumerable<IValueReference> GetValues()
		{
			this.EnsureInitialized();
			foreach (SapBwMemberProvider sapBwMemberProvider in this.valueProviders)
			{
				foreach (IValueReference valueReference in sapBwMemberProvider.GetValues(0L))
				{
					yield return valueReference;
				}
				IEnumerator<IValueReference> enumerator2 = null;
			}
			List<SapBwMemberProvider>.Enumerator enumerator = default(List<SapBwMemberProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000020FA File Offset: 0x000002FA
		protected override Dictionary<string, MdxHierarchy> GetExternalHierarchies(SapBwVariable variable)
		{
			return null;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0007BBEA File Offset: 0x00079DEA
		protected override void EnsureInitialized()
		{
			if (this.valueProviders == null)
			{
				this.valueProviders = new List<SapBwMemberProvider>();
				base.BuildValueProviders();
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0007BC08 File Offset: 0x00079E08
		protected override void AddValueProvider(MdxHierarchy defaultHierarchy, Dictionary<string, MdxHierarchy> hierarchyNames)
		{
			if (defaultHierarchy != null)
			{
				this.valueProviders.Add(new SapBwMemberProvider(this, null, defaultHierarchy.MdxIdentifier));
				return;
			}
			foreach (MdxHierarchy mdxHierarchy in hierarchyNames.Values)
			{
				this.valueProviders.Add(new SapBwMemberProvider(this, null, mdxHierarchy.MdxIdentifier));
			}
		}

		// Token: 0x040011F7 RID: 4599
		private List<SapBwMemberProvider> valueProviders;
	}
}
