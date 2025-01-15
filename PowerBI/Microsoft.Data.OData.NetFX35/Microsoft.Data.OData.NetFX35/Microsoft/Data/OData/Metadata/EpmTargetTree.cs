using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000214 RID: 532
	internal sealed class EpmTargetTree
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x000394B5 File Offset: 0x000376B5
		internal EpmTargetTree()
		{
			this.syndicationRoot = new EpmTargetPathSegment();
			this.nonSyndicationRoot = new EpmTargetPathSegment();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000394D3 File Offset: 0x000376D3
		internal EpmTargetPathSegment SyndicationRoot
		{
			get
			{
				return this.syndicationRoot;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000394DB File Offset: 0x000376DB
		internal EpmTargetPathSegment NonSyndicationRoot
		{
			get
			{
				return this.nonSyndicationRoot;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000394E3 File Offset: 0x000376E3
		internal ODataVersion MinimumODataProtocolVersion
		{
			get
			{
				if (this.countOfNonContentV2Mappings > 0)
				{
					return ODataVersion.V2;
				}
				return ODataVersion.V1;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00039550 File Offset: 0x00037750
		internal void Add(EntityPropertyMappingInfo epmInfo)
		{
			string targetPath = epmInfo.Attribute.TargetPath;
			string namespaceUri = epmInfo.Attribute.TargetNamespaceUri;
			string targetNamespacePrefix = epmInfo.Attribute.TargetNamespacePrefix;
			EpmTargetPathSegment epmTargetPathSegment = (epmInfo.IsSyndicationMapping ? this.SyndicationRoot : this.NonSyndicationRoot);
			IList<EpmTargetPathSegment> list = epmTargetPathSegment.SubSegments;
			string[] array = targetPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				string targetSegment = array[i];
				if (targetSegment.Length == 0)
				{
					throw new ODataException(Strings.EpmTargetTree_InvalidTargetPath_EmptySegment(targetPath));
				}
				if (targetSegment.get_Chars(0) == '@' && i != array.Length - 1)
				{
					throw new ODataException(Strings.EpmTargetTree_AttributeInMiddle(targetSegment));
				}
				EpmTargetPathSegment epmTargetPathSegment2 = Enumerable.SingleOrDefault<EpmTargetPathSegment>(list, (EpmTargetPathSegment segment) => segment.SegmentName == targetSegment && (epmInfo.IsSyndicationMapping || segment.SegmentNamespaceUri == namespaceUri));
				if (epmTargetPathSegment2 != null)
				{
					epmTargetPathSegment = epmTargetPathSegment2;
				}
				else
				{
					epmTargetPathSegment = new EpmTargetPathSegment(targetSegment, namespaceUri, targetNamespacePrefix, epmTargetPathSegment);
					if (targetSegment.get_Chars(0) == '@')
					{
						list.Insert(0, epmTargetPathSegment);
					}
					else
					{
						list.Add(epmTargetPathSegment);
					}
				}
				list = epmTargetPathSegment.SubSegments;
			}
			if (epmTargetPathSegment.EpmInfo != null)
			{
				throw new ODataException(Strings.EpmTargetTree_DuplicateEpmAttributesWithSameTargetName(epmTargetPathSegment.EpmInfo.DefiningType.ODataFullName(), EpmTargetTree.GetPropertyNameFromEpmInfo(epmTargetPathSegment.EpmInfo), epmTargetPathSegment.EpmInfo.Attribute.SourcePath, epmInfo.Attribute.SourcePath));
			}
			if (!epmInfo.Attribute.KeepInContent)
			{
				this.countOfNonContentV2Mappings++;
			}
			epmTargetPathSegment.EpmInfo = epmInfo;
			List<EntityPropertyMappingAttribute> list2 = new List<EntityPropertyMappingAttribute>(2);
			if (EpmTargetTree.HasMixedContent(this.NonSyndicationRoot, list2))
			{
				throw new ODataException(Strings.EpmTargetTree_InvalidTargetPath_MixedContent(list2[0].TargetPath, list2[1].TargetPath));
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000397D8 File Offset: 0x000379D8
		internal void Remove(EntityPropertyMappingInfo epmInfo)
		{
			string targetPath = epmInfo.Attribute.TargetPath;
			string namespaceUri = epmInfo.Attribute.TargetNamespaceUri;
			EpmTargetPathSegment epmTargetPathSegment = (epmInfo.IsSyndicationMapping ? this.SyndicationRoot : this.NonSyndicationRoot);
			List<EpmTargetPathSegment> list = epmTargetPathSegment.SubSegments;
			string[] array = targetPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				string targetSegment = array[i];
				EpmTargetPathSegment epmTargetPathSegment2 = Enumerable.FirstOrDefault<EpmTargetPathSegment>(list, (EpmTargetPathSegment segment) => segment.SegmentName == targetSegment && (epmInfo.IsSyndicationMapping || segment.SegmentNamespaceUri == namespaceUri));
				if (epmTargetPathSegment2 == null)
				{
					return;
				}
				epmTargetPathSegment = epmTargetPathSegment2;
				list = epmTargetPathSegment.SubSegments;
			}
			if (epmTargetPathSegment.EpmInfo != null)
			{
				if (!epmTargetPathSegment.EpmInfo.Attribute.KeepInContent)
				{
					this.countOfNonContentV2Mappings--;
				}
				do
				{
					EpmTargetPathSegment parentSegment = epmTargetPathSegment.ParentSegment;
					parentSegment.SubSegments.Remove(epmTargetPathSegment);
					epmTargetPathSegment = parentSegment;
				}
				while (epmTargetPathSegment.ParentSegment != null && !epmTargetPathSegment.HasContent && epmTargetPathSegment.SubSegments.Count == 0);
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003990D File Offset: 0x00037B0D
		[Conditional("DEBUG")]
		internal void Validate()
		{
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003991C File Offset: 0x00037B1C
		private static bool HasMixedContent(EpmTargetPathSegment currentSegment, List<EntityPropertyMappingAttribute> ancestorsWithContent)
		{
			foreach (EpmTargetPathSegment epmTargetPathSegment in Enumerable.Where<EpmTargetPathSegment>(currentSegment.SubSegments, (EpmTargetPathSegment s) => !s.IsAttribute))
			{
				if (epmTargetPathSegment.HasContent && ancestorsWithContent.Count == 1)
				{
					ancestorsWithContent.Add(epmTargetPathSegment.EpmInfo.Attribute);
					return true;
				}
				if (epmTargetPathSegment.HasContent)
				{
					ancestorsWithContent.Add(epmTargetPathSegment.EpmInfo.Attribute);
				}
				if (EpmTargetTree.HasMixedContent(epmTargetPathSegment, ancestorsWithContent))
				{
					return true;
				}
				if (epmTargetPathSegment.HasContent)
				{
					ancestorsWithContent.Clear();
				}
			}
			return false;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000399E4 File Offset: 0x00037BE4
		private static string GetPropertyNameFromEpmInfo(EntityPropertyMappingInfo epmInfo)
		{
			if (epmInfo.Attribute.TargetSyndicationItem == SyndicationItemProperty.CustomProperty)
			{
				return epmInfo.Attribute.TargetPath;
			}
			return epmInfo.Attribute.TargetSyndicationItem.ToString();
		}

		// Token: 0x04000602 RID: 1538
		private readonly EpmTargetPathSegment syndicationRoot;

		// Token: 0x04000603 RID: 1539
		private readonly EpmTargetPathSegment nonSyndicationRoot;

		// Token: 0x04000604 RID: 1540
		private int countOfNonContentV2Mappings;
	}
}
