using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x02000121 RID: 289
	public class ODataMediaTypeResolver
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x00027918 File Offset: 0x00025B18
		public ODataMediaTypeResolver()
			: this(false)
		{
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00027921 File Offset: 0x00025B21
		private ODataMediaTypeResolver(bool enableAtom)
		{
			this.mediaTypesForPayloadKind = ODataMediaTypeResolver.CloneDefaultMediaTypes(enableAtom);
			this.AddJsonLightMediaTypes();
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002793B File Offset: 0x00025B3B
		internal static ODataMediaTypeResolver DefaultMediaTypeResolver
		{
			get
			{
				return ODataMediaTypeResolver.MediaTypeResolverWithoutAtom;
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00027942 File Offset: 0x00025B42
		public virtual IEnumerable<ODataMediaTypeFormat> GetMediaTypeFormats(ODataPayloadKind payloadKind)
		{
			return this.mediaTypesForPayloadKind[(int)payloadKind];
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002794C File Offset: 0x00025B4C
		internal static ODataMediaTypeResolver GetMediaTypeResolver(bool enableAtom)
		{
			if (!enableAtom)
			{
				return ODataMediaTypeResolver.MediaTypeResolverWithoutAtom;
			}
			return ODataMediaTypeResolver.MediaTypeResolverWithAtom;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002795C File Offset: 0x00025B5C
		private static List<ODataMediaTypeFormat>[] CloneDefaultMediaTypes(bool includeAtom)
		{
			ODataMediaTypeFormat[][] array = (includeAtom ? ODataMediaTypeResolver.defaultMediaTypes : ODataMediaTypeResolver.defaultMediaTypesWithoutAtom);
			List<ODataMediaTypeFormat>[] array2 = new List<ODataMediaTypeFormat>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new List<ODataMediaTypeFormat>(array[i]);
			}
			return array2;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00027AC4 File Offset: 0x00025CC4
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

		// Token: 0x0400045B RID: 1115
		private static readonly ODataMediaType ApplicationAtomXmlMediaType = new ODataMediaType("application", "atom+xml");

		// Token: 0x0400045C RID: 1116
		private static readonly ODataMediaType ApplicationXmlMediaType = new ODataMediaType("application", "xml");

		// Token: 0x0400045D RID: 1117
		private static readonly ODataMediaType TextXmlMediaType = new ODataMediaType("text", "xml");

		// Token: 0x0400045E RID: 1118
		private static readonly ODataMediaType ApplicationJsonMediaType = new ODataMediaType("application", "json");

		// Token: 0x0400045F RID: 1119
		private static readonly ODataMediaTypeFormat[][] defaultMediaTypes = new ODataMediaTypeFormat[][]
		{
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "atom+xml", new KeyValuePair<string, string>("type", "feed")), ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationAtomXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "atom+xml", new KeyValuePair<string, string>("type", "entry")), ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationAtomXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.TextXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.TextXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "atom+xml", new KeyValuePair<string, string>("type", "feed")), ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationAtomXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("text", "plain"), ODataFormat.RawValue)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "octet-stream"), ODataFormat.RawValue)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.TextXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom),
				new ODataMediaTypeFormat(new ODataMediaType("application", "atomsvc+xml"), ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Metadata)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("multipart", "mixed"), ODataFormat.Batch)
			},
			new ODataMediaTypeFormat[0],
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationXmlMediaType, ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.TextXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "atom+xml", new KeyValuePair<string, string>("type", "feed")), ODataFormat.Atom),
				new ODataMediaTypeFormat(ODataMediaTypeResolver.ApplicationAtomXmlMediaType, ODataFormat.Atom)
			},
			new ODataMediaTypeFormat[]
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "http"), ODataFormat.RawValue)
			}
		};

		// Token: 0x04000460 RID: 1120
		private static readonly ODataMediaTypeFormat[][] defaultMediaTypesWithoutAtom = new ODataMediaTypeFormat[][]
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

		// Token: 0x04000461 RID: 1121
		private readonly List<ODataMediaTypeFormat>[] mediaTypesForPayloadKind;

		// Token: 0x04000462 RID: 1122
		private static readonly ODataPayloadKind[] JsonPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.Feed,
			ODataPayloadKind.Entry,
			ODataPayloadKind.Property,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.Collection,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter,
			ODataPayloadKind.IndividualProperty
		};

		// Token: 0x04000463 RID: 1123
		private static readonly ODataMediaTypeResolver MediaTypeResolverWithoutAtom = new ODataMediaTypeResolver(false);

		// Token: 0x04000464 RID: 1124
		private static readonly ODataMediaTypeResolver MediaTypeResolverWithAtom = new ODataMediaTypeResolver(true);
	}
}
