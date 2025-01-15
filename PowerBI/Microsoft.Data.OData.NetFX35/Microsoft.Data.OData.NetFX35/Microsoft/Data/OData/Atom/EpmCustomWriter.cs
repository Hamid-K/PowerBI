using System;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000265 RID: 613
	internal sealed class EpmCustomWriter : EpmWriter
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00047BA7 File Offset: 0x00045DA7
		private EpmCustomWriter(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00047BB0 File Offset: 0x00045DB0
		internal static void WriteEntryEpm(XmlWriter writer, EpmTargetTree epmTargetTree, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ODataAtomOutputContext atomOutputContext)
		{
			EpmCustomWriter epmCustomWriter = new EpmCustomWriter(atomOutputContext);
			epmCustomWriter.WriteEntryEpm(writer, epmTargetTree, epmValueCache, entityType);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00047BCF File Offset: 0x00045DCF
		private static void WriteNamespaceDeclaration(XmlWriter writer, EpmTargetPathSegment targetSegment, ref string alreadyDeclaredPrefix)
		{
			if (alreadyDeclaredPrefix == null)
			{
				writer.WriteAttributeString("xmlns", targetSegment.SegmentNamespacePrefix, "http://www.w3.org/2000/xmlns/", targetSegment.SegmentNamespaceUri);
				alreadyDeclaredPrefix = targetSegment.SegmentNamespacePrefix;
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00047BFC File Offset: 0x00045DFC
		private void WriteEntryEpm(XmlWriter writer, EpmTargetTree epmTargetTree, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			EpmTargetPathSegment nonSyndicationRoot = epmTargetTree.NonSyndicationRoot;
			if (nonSyndicationRoot.SubSegments.Count == 0)
			{
				return;
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in nonSyndicationRoot.SubSegments)
			{
				string text = null;
				this.WriteElementEpm(writer, epmTargetPathSegment, epmValueCache, entityType, ref text);
			}
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00047C6C File Offset: 0x00045E6C
		private void WriteElementEpm(XmlWriter writer, EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ref string alreadyDeclaredPrefix)
		{
			string text = targetSegment.SegmentNamespacePrefix ?? string.Empty;
			writer.WriteStartElement(text, targetSegment.SegmentName, targetSegment.SegmentNamespaceUri);
			if (text.Length > 0)
			{
				EpmCustomWriter.WriteNamespaceDeclaration(writer, targetSegment, ref alreadyDeclaredPrefix);
			}
			foreach (EpmTargetPathSegment epmTargetPathSegment in targetSegment.SubSegments)
			{
				if (epmTargetPathSegment.IsAttribute)
				{
					this.WriteAttributeEpm(writer, epmTargetPathSegment, epmValueCache, entityType, ref alreadyDeclaredPrefix);
				}
			}
			if (targetSegment.HasContent)
			{
				string entryPropertyValueAsText = this.GetEntryPropertyValueAsText(targetSegment, epmValueCache, entityType);
				ODataAtomWriterUtils.WriteString(writer, entryPropertyValueAsText);
			}
			else
			{
				foreach (EpmTargetPathSegment epmTargetPathSegment2 in targetSegment.SubSegments)
				{
					if (!epmTargetPathSegment2.IsAttribute)
					{
						this.WriteElementEpm(writer, epmTargetPathSegment2, epmValueCache, entityType, ref alreadyDeclaredPrefix);
					}
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00047D78 File Offset: 0x00045F78
		private void WriteAttributeEpm(XmlWriter writer, EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType, ref string alreadyDeclaredPrefix)
		{
			string entryPropertyValueAsText = this.GetEntryPropertyValueAsText(targetSegment, epmValueCache, entityType);
			string text = targetSegment.SegmentNamespacePrefix ?? string.Empty;
			writer.WriteAttributeString(text, targetSegment.AttributeName, targetSegment.SegmentNamespaceUri, entryPropertyValueAsText);
			if (text.Length > 0)
			{
				EpmCustomWriter.WriteNamespaceDeclaration(writer, targetSegment, ref alreadyDeclaredPrefix);
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00047DC8 File Offset: 0x00045FC8
		private string GetEntryPropertyValueAsText(EpmTargetPathSegment targetSegment, EntryPropertiesValueCache epmValueCache, IEdmEntityTypeReference entityType)
		{
			object obj = base.ReadEntryPropertyValue(targetSegment.EpmInfo, epmValueCache, entityType);
			if (obj == null)
			{
				return string.Empty;
			}
			return EpmWriterUtils.GetPropertyValueAsText(obj);
		}
	}
}
