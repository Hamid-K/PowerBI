using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B3 RID: 435
	internal sealed class MediaTypeResolver
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002C80C File Offset: 0x0002AA0C
		private MediaTypeResolver(ODataVersion version)
		{
			this.version = version;
			this.mediaTypesForPayloadKind = MediaTypeResolver.CloneDefaultMediaTypes();
			if (this.version < ODataVersion.V3)
			{
				MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonMediaType
				};
				this.AddForJsonPayloadKinds(mediaTypeWithFormat);
				return;
			}
			this.AddJsonLightMediaTypes();
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002C866 File Offset: 0x0002AA66
		private MediaTypeResolver(ODataVersion version, ODataBehaviorKind formatBehaviorKind)
			: this(version)
		{
			if (this.version < ODataVersion.V3 && formatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				this.AddV2ClientMediaTypes();
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002C882 File Offset: 0x0002AA82
		public static MediaTypeResolver DefaultMediaTypeResolver
		{
			get
			{
				return MediaTypeResolver.MediaTypeResolverCache[ODataVersion.V1];
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002C88F File Offset: 0x0002AA8F
		internal static MediaTypeResolver GetWriterMediaTypeResolver(ODataVersion version)
		{
			return MediaTypeResolver.MediaTypeResolverCache[version];
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002C89C File Offset: 0x0002AA9C
		internal static MediaTypeResolver CreateReaderMediaTypeResolver(ODataVersion version, ODataBehaviorKind formatBehaviorKind)
		{
			return new MediaTypeResolver(version, formatBehaviorKind);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002C8A5 File Offset: 0x0002AAA5
		internal IList<MediaTypeWithFormat> GetMediaTypesForPayloadKind(ODataPayloadKind payloadKind)
		{
			return this.mediaTypesForPayloadKind[(int)payloadKind];
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
		internal bool IsIllegalMediaType(MediaType mediaType)
		{
			return this.version < ODataVersion.V3 && HttpUtils.CompareMediaTypeNames(mediaType.SubTypeName, "json") && HttpUtils.CompareMediaTypeNames(mediaType.TypeName, "application") && (mediaType.MediaTypeHasParameterWithValue("odata", "minimalmetadata") || mediaType.MediaTypeHasParameterWithValue("odata", "fullmetadata") || mediaType.MediaTypeHasParameterWithValue("odata", "nometadata"));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002C924 File Offset: 0x0002AB24
		private static IList<MediaTypeWithFormat>[] CloneDefaultMediaTypes()
		{
			IList<MediaTypeWithFormat>[] array = new IList<MediaTypeWithFormat>[MediaTypeResolver.defaultMediaTypes.Length];
			for (int i = 0; i < MediaTypeResolver.defaultMediaTypes.Length; i++)
			{
				array[i] = new List<MediaTypeWithFormat>(MediaTypeResolver.defaultMediaTypes[i]);
			}
			return array;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002C960 File Offset: 0x0002AB60
		private static void AddMediaTypeEntryOrdered(IList<MediaTypeWithFormat> mediaTypeList, MediaTypeWithFormat mediaTypeToInsert, ODataFormat beforeFormat)
		{
			for (int i = 0; i < mediaTypeList.Count; i++)
			{
				if (mediaTypeList[i].Format == beforeFormat)
				{
					mediaTypeList.Insert(i, mediaTypeToInsert);
					return;
				}
			}
			mediaTypeList.Add(mediaTypeToInsert);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		private void AddJsonLightMediaTypes()
		{
			var array = new <>f__AnonymousType0<string, string[]>[]
			{
				new
				{
					ParameterName = "odata",
					Values = new string[] { "minimalmetadata", "fullmetadata", "nometadata" }
				},
				new
				{
					ParameterName = "streaming",
					Values = new string[] { "true", "false" }
				}
			};
			LinkedList<MediaType> linkedList = new LinkedList<MediaType>();
			linkedList.AddFirst(MediaTypeResolver.ApplicationJsonMediaType);
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				for (LinkedListNode<MediaType> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					MediaType value = linkedListNode.Value;
					foreach (string text in <>f__AnonymousType.Values)
					{
						List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(value.Parameters ?? Enumerable.Empty<KeyValuePair<string, string>>());
						list.Add(new KeyValuePair<string, string>(<>f__AnonymousType.ParameterName, text));
						List<KeyValuePair<string, string>> list2 = list;
						MediaType mediaType = new MediaType(value.TypeName, value.SubTypeName, list2);
						linkedList.AddBefore(linkedListNode, mediaType);
					}
				}
			}
			foreach (MediaType mediaType2 in linkedList)
			{
				MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
				{
					Format = ODataFormat.Json,
					MediaType = mediaType2
				};
				if (mediaType2 == MediaTypeResolver.ApplicationJsonMediaType)
				{
					this.AddForJsonPayloadKinds(mediaTypeWithFormat);
				}
				else
				{
					this.InsertForJsonPayloadKinds(mediaTypeWithFormat, ODataFormat.VerboseJson);
				}
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002CC70 File Offset: 0x0002AE70
		private void AddForJsonPayloadKinds(MediaTypeWithFormat mediaTypeWithFormat)
		{
			foreach (ODataPayloadKind odataPayloadKind in MediaTypeResolver.JsonPayloadKinds)
			{
				this.mediaTypesForPayloadKind[(int)odataPayloadKind].Add(mediaTypeWithFormat);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		private void InsertForJsonPayloadKinds(MediaTypeWithFormat mediaTypeWithFormat, ODataFormat beforeFormat)
		{
			foreach (ODataPayloadKind odataPayloadKind in MediaTypeResolver.JsonPayloadKinds)
			{
				MediaTypeResolver.AddMediaTypeEntryOrdered(this.mediaTypesForPayloadKind[(int)odataPayloadKind], mediaTypeWithFormat, beforeFormat);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		private void AddV2ClientMediaTypes()
		{
			MediaTypeWithFormat mediaTypeWithFormat = new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
			};
			MediaTypeWithFormat mediaTypeWithFormat2 = new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = MediaTypeResolver.ApplicationXmlMediaType
			};
			IList<MediaTypeWithFormat> list = this.mediaTypesForPayloadKind[0];
			list.Add(new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = new MediaType("application", "xml", new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("type", "feed")
				})
			});
			list.Add(mediaTypeWithFormat2);
			IList<MediaTypeWithFormat> list2 = this.mediaTypesForPayloadKind[1];
			list2.Add(new MediaTypeWithFormat
			{
				Format = ODataFormat.Atom,
				MediaType = new MediaType("application", "xml", new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("type", "entry")
				})
			});
			list2.Add(mediaTypeWithFormat2);
			this.mediaTypesForPayloadKind[2].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[3].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[4].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[7].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[8].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[9].Add(mediaTypeWithFormat);
			this.mediaTypesForPayloadKind[10].Add(mediaTypeWithFormat);
		}

		// Token: 0x04000480 RID: 1152
		private static readonly MediaType ApplicationAtomXmlMediaType = new MediaType("application", "atom+xml");

		// Token: 0x04000481 RID: 1153
		private static readonly MediaType ApplicationXmlMediaType = new MediaType("application", "xml");

		// Token: 0x04000482 RID: 1154
		private static readonly MediaType TextXmlMediaType = new MediaType("text", "xml");

		// Token: 0x04000483 RID: 1155
		private static readonly MediaType ApplicationJsonMediaType = new MediaType("application", "json");

		// Token: 0x04000484 RID: 1156
		private static readonly MediaType ApplicationJsonVerboseMediaType = new MediaType("application", "json", new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("odata", "verbose")
		});

		// Token: 0x04000485 RID: 1157
		private static readonly MediaTypeWithFormat[][] defaultMediaTypes = new MediaTypeWithFormat[][]
		{
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atom+xml", new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("type", "feed")
					})
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atom+xml", new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("type", "entry")
					})
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationAtomXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.RawValue,
					MediaType = new MediaType("text", "plain")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.RawValue,
					MediaType = new MediaType("application", "octet-stream")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.TextXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = new MediaType("application", "atomsvc+xml")
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Metadata,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Atom,
					MediaType = MediaTypeResolver.ApplicationXmlMediaType
				},
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.Batch,
					MediaType = new MediaType("multipart", "mixed")
				}
			},
			new MediaTypeWithFormat[]
			{
				new MediaTypeWithFormat
				{
					Format = ODataFormat.VerboseJson,
					MediaType = MediaTypeResolver.ApplicationJsonVerboseMediaType
				}
			}
		};

		// Token: 0x04000486 RID: 1158
		private static readonly ODataVersionCache<MediaTypeResolver> MediaTypeResolverCache = new ODataVersionCache<MediaTypeResolver>((ODataVersion version) => new MediaTypeResolver(version));

		// Token: 0x04000487 RID: 1159
		private readonly ODataVersion version;

		// Token: 0x04000488 RID: 1160
		private readonly IList<MediaTypeWithFormat>[] mediaTypesForPayloadKind;

		// Token: 0x04000489 RID: 1161
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
			ODataPayloadKind.Parameter
		};
	}
}
