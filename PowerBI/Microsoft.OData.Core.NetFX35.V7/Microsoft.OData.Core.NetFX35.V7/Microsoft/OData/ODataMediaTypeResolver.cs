using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000021 RID: 33
	public class ODataMediaTypeResolver
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000042A0 File Offset: 0x000024A0
		public ODataMediaTypeResolver()
		{
			this.mediaTypesForPayloadKind = ODataMediaTypeResolver.CloneDefaultMediaTypes();
			this.AddJsonLightMediaTypes();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000042B9 File Offset: 0x000024B9
		public virtual IEnumerable<ODataMediaTypeFormat> GetMediaTypeFormats(ODataPayloadKind payloadKind)
		{
			return this.mediaTypesForPayloadKind[(int)payloadKind];
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000042C3 File Offset: 0x000024C3
		internal static ODataMediaTypeResolver GetMediaTypeResolver(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataMediaTypeResolver.MediaTypeResolver;
			}
			return container.GetRequiredService<ODataMediaTypeResolver>();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000042D4 File Offset: 0x000024D4
		private static List<ODataMediaTypeFormat>[] CloneDefaultMediaTypes()
		{
			ODataMediaTypeFormat[][] array = ODataMediaTypeResolver.defaultMediaTypes;
			List<ODataMediaTypeFormat>[] array2 = new List<ODataMediaTypeFormat>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new List<ODataMediaTypeFormat>(array[i]);
			}
			return array2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000430C File Offset: 0x0000250C
		private void AddJsonLightMediaTypes()
		{
			var array = new <>f__AnonymousType0<string, string[]>[]
			{
				new
				{
					ParameterName = "odata.metadata",
					Values = new string[] { "minimal", "full", "none" }
				},
				new
				{
					ParameterName = "odata.streaming",
					Values = new string[] { "true", "false" }
				},
				new
				{
					ParameterName = "IEEE754Compatible",
					Values = new string[] { "false", "true" }
				}
			};
			LinkedList<ODataMediaType> linkedList = new LinkedList<ODataMediaType>();
			linkedList.AddFirst(ODataMediaTypeResolver.ApplicationJsonMediaType);
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				for (LinkedListNode<ODataMediaType> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					ODataMediaType value = linkedListNode.Value;
					foreach (string text in <>f__AnonymousType.Values)
					{
						List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(value.Parameters ?? Enumerable.Empty<KeyValuePair<string, string>>());
						list.Add(new KeyValuePair<string, string>(<>f__AnonymousType.ParameterName, text));
						List<KeyValuePair<string, string>> list2 = list;
						ODataMediaType odataMediaType = new ODataMediaType(value.Type, value.SubType, list2);
						linkedList.AddBefore(linkedListNode, odataMediaType);
					}
				}
			}
			List<ODataMediaTypeFormat> list3 = Enumerable.ToList<ODataMediaTypeFormat>(Enumerable.Select<ODataMediaType, ODataMediaTypeFormat>(linkedList, (ODataMediaType mediaType) => new ODataMediaTypeFormat(mediaType, ODataFormat.Json)));
			foreach (ODataPayloadKind odataPayloadKind in ODataMediaTypeResolver.JsonPayloadKinds)
			{
				this.mediaTypesForPayloadKind[(int)odataPayloadKind].InsertRange(0, list3);
			}
		}

		// Token: 0x04000089 RID: 137
		private static readonly ODataMediaType ApplicationXmlMediaType = new ODataMediaType("application", "xml");

		// Token: 0x0400008A RID: 138
		private static readonly ODataMediaType ApplicationJsonMediaType = new ODataMediaType("application", "json");

		// Token: 0x0400008B RID: 139
		private static readonly ODataMediaTypeFormat[][] defaultMediaTypes = new ODataMediaTypeFormat[][]
		{
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("text", "plain"), ODataFormat.RawValue)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "octet-stream"), ODataFormat.RawValue)
			},
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Metadata)
			},
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("multipart", "mixed"), ODataFormat.Batch)
			},
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "http"), ODataFormat.RawValue)
			}
		};

		// Token: 0x0400008C RID: 140
		private readonly List<ODataMediaTypeFormat>[] mediaTypesForPayloadKind;

		// Token: 0x0400008D RID: 141
		private static readonly ODataPayloadKind[] JsonPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.ResourceSet,
			ODataPayloadKind.Resource,
			ODataPayloadKind.Property,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.Collection,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter,
			ODataPayloadKind.IndividualProperty
		};

		// Token: 0x0400008E RID: 142
		private static readonly ODataMediaTypeResolver MediaTypeResolver = new ODataMediaTypeResolver();
	}
}
