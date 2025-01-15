using System;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Common
{
	// Token: 0x020001EF RID: 495
	[AttributeUsage(4, AllowMultiple = true, Inherited = true)]
	public sealed class EntityPropertyMappingAttribute : Attribute
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00033DB0 File Offset: 0x00031FB0
		public EntityPropertyMappingAttribute(string sourcePath, SyndicationItemProperty targetSyndicationItem, SyndicationTextContentKind targetTextContentKind, bool keepInContent)
		{
			if (string.IsNullOrEmpty(sourcePath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("sourcePath"));
			}
			this.sourcePath = sourcePath;
			this.targetPath = targetSyndicationItem.ToTargetPath();
			this.targetSyndicationItem = targetSyndicationItem;
			this.targetTextContentKind = targetTextContentKind;
			this.targetNamespacePrefix = "atom";
			this.targetNamespaceUri = "http://www.w3.org/2005/Atom";
			this.keepInContent = keepInContent;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00033E1C File Offset: 0x0003201C
		public EntityPropertyMappingAttribute(string sourcePath, string targetPath, string targetNamespacePrefix, string targetNamespaceUri, bool keepInContent)
		{
			if (string.IsNullOrEmpty(sourcePath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("sourcePath"));
			}
			this.sourcePath = sourcePath;
			if (string.IsNullOrEmpty(targetPath))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetPath"));
			}
			if (targetPath.get_Chars(0) == '@')
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_InvalidTargetPath(targetPath));
			}
			this.targetPath = targetPath;
			this.targetSyndicationItem = SyndicationItemProperty.CustomProperty;
			this.targetTextContentKind = SyndicationTextContentKind.Plaintext;
			this.targetNamespacePrefix = targetNamespacePrefix;
			if (string.IsNullOrEmpty(targetNamespaceUri))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetNamespaceUri"));
			}
			this.targetNamespaceUri = targetNamespaceUri;
			Uri uri;
			if (!Uri.TryCreate(targetNamespaceUri, 1, ref uri))
			{
				throw new ArgumentException(Strings.EntityPropertyMapping_TargetNamespaceUriNotValid(targetNamespaceUri));
			}
			this.keepInContent = keepInContent;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00033EDB File Offset: 0x000320DB
		public string SourcePath
		{
			get
			{
				return this.sourcePath;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00033EE3 File Offset: 0x000320E3
		public string TargetPath
		{
			get
			{
				return this.targetPath;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00033EEB File Offset: 0x000320EB
		public SyndicationItemProperty TargetSyndicationItem
		{
			get
			{
				return this.targetSyndicationItem;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00033EF3 File Offset: 0x000320F3
		public string TargetNamespacePrefix
		{
			get
			{
				return this.targetNamespacePrefix;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00033EFB File Offset: 0x000320FB
		public string TargetNamespaceUri
		{
			get
			{
				return this.targetNamespaceUri;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00033F03 File Offset: 0x00032103
		public SyndicationTextContentKind TargetTextContentKind
		{
			get
			{
				return this.targetTextContentKind;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00033F0B File Offset: 0x0003210B
		public bool KeepInContent
		{
			get
			{
				return this.keepInContent;
			}
		}

		// Token: 0x04000549 RID: 1353
		private readonly string sourcePath;

		// Token: 0x0400054A RID: 1354
		private readonly string targetPath;

		// Token: 0x0400054B RID: 1355
		private readonly SyndicationItemProperty targetSyndicationItem;

		// Token: 0x0400054C RID: 1356
		private readonly SyndicationTextContentKind targetTextContentKind;

		// Token: 0x0400054D RID: 1357
		private readonly string targetNamespacePrefix;

		// Token: 0x0400054E RID: 1358
		private readonly string targetNamespaceUri;

		// Token: 0x0400054F RID: 1359
		private readonly bool keepInContent;
	}
}
