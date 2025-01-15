using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C7 RID: 455
	internal sealed class ODataVerboseJsonEntryAndFeedSerializer : ODataVerboseJsonPropertyAndValueSerializer
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		internal ODataVerboseJsonEntryAndFeedSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		internal void WriteEntryMetadata(ODataEntry entry, ProjectedPropertiesAnnotation projectedProperties, IEdmEntityType entryEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			base.JsonWriter.WriteName("__metadata");
			base.JsonWriter.StartObjectScope();
			string id = entry.Id;
			if (id != null)
			{
				base.JsonWriter.WriteName("id");
				base.JsonWriter.WriteValue(id);
			}
			Uri uri = entry.EditLink ?? entry.ReadLink;
			if (uri != null)
			{
				base.JsonWriter.WriteName("uri");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(uri));
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				base.WriteETag("etag", etag);
			}
			string entryTypeNameForWriting = base.VerboseJsonOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entry);
			if (entryTypeNameForWriting != null)
			{
				base.JsonWriter.WriteName("type");
				base.JsonWriter.WriteValue(entryTypeNameForWriting);
			}
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource != null)
			{
				WriterValidationUtils.ValidateStreamReferenceValue(mediaResource, true);
				base.WriteStreamReferenceValueContent(mediaResource);
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(actions), "actions", true, false);
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				this.WriteOperations(Enumerable.Cast<ODataOperation>(functions), "functions", false, false);
			}
			IEnumerable<ODataAssociationLink> associationLinks = entry.AssociationLinks;
			if (associationLinks != null)
			{
				bool flag = true;
				foreach (ODataAssociationLink odataAssociationLink in associationLinks)
				{
					ValidationUtils.ValidateAssociationLinkNotNull(odataAssociationLink);
					if (!projectedProperties.ShouldSkipProperty(odataAssociationLink.Name))
					{
						if (flag)
						{
							base.JsonWriter.WriteName("properties");
							base.JsonWriter.StartObjectScope();
							flag = false;
						}
						base.ValidateAssociationLink(odataAssociationLink, entryEntityType);
						this.WriteAssociationLink(odataAssociationLink, duplicatePropertyNamesChecker);
					}
				}
				if (!flag)
				{
					base.JsonWriter.EndObjectScope();
				}
			}
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002FC00 File Offset: 0x0002DE00
		internal void WriteOperations(IEnumerable<ODataOperation> operations, string operationName, bool isAction, bool writingJsonLight)
		{
			bool flag = true;
			IEnumerable<IGrouping<string, ODataOperation>> enumerable = Enumerable.GroupBy<ODataOperation, string>(operations, delegate(ODataOperation o)
			{
				ValidationUtils.ValidateOperationNotNull(o, isAction);
				WriterValidationUtils.ValidateCanWriteOperation(o, this.VerboseJsonOutputContext.WritingResponse);
				ValidationUtils.ValidateOperationMetadataNotNull(o);
				if (!writingJsonLight)
				{
					ValidationUtils.ValidateOperationTargetNotNull(o);
				}
				return this.UriToUriString(o.Metadata, false);
			});
			foreach (IGrouping<string, ODataOperation> grouping in enumerable)
			{
				if (flag)
				{
					base.VerboseJsonOutputContext.JsonWriter.WriteName(operationName);
					base.VerboseJsonOutputContext.JsonWriter.StartObjectScope();
					flag = false;
				}
				this.WriteOperationMetadataGroup(grouping);
			}
			if (!flag)
			{
				base.VerboseJsonOutputContext.JsonWriter.EndObjectScope();
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002FCB8 File Offset: 0x0002DEB8
		private void WriteAssociationLink(ODataAssociationLink associationLink, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLink);
			base.JsonWriter.WriteName(associationLink.Name);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("associationuri");
			base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(associationLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002FD1C File Offset: 0x0002DF1C
		private void WriteOperationMetadataGroup(IGrouping<string, ODataOperation> operations)
		{
			bool flag = true;
			foreach (ODataOperation odataOperation in operations)
			{
				if (flag)
				{
					base.VerboseJsonOutputContext.JsonWriter.WriteName(operations.Key);
					base.VerboseJsonOutputContext.JsonWriter.StartArrayScope();
					flag = false;
				}
				this.WriteOperation(odataOperation);
			}
			base.VerboseJsonOutputContext.JsonWriter.EndArrayScope();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002FDA4 File Offset: 0x0002DFA4
		private void WriteOperation(ODataOperation operation)
		{
			base.VerboseJsonOutputContext.JsonWriter.StartObjectScope();
			if (operation.Title != null)
			{
				base.VerboseJsonOutputContext.JsonWriter.WriteName("title");
				base.VerboseJsonOutputContext.JsonWriter.WriteValue(operation.Title);
			}
			if (operation.Target != null)
			{
				string text = base.UriToAbsoluteUriString(operation.Target);
				base.VerboseJsonOutputContext.JsonWriter.WriteName("target");
				base.VerboseJsonOutputContext.JsonWriter.WriteValue(text);
			}
			base.VerboseJsonOutputContext.JsonWriter.EndObjectScope();
		}
	}
}
