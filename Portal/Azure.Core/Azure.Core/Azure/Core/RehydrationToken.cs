using System;
using System.ClientModel.Primitives;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Azure.Core
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct RehydrationToken : IJsonModel<RehydrationToken>, IPersistableModel<RehydrationToken>, IJsonModel<object>, IPersistableModel<object>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008092 File Offset: 0x00006292
		[Nullable(2)]
		public string Id
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000809A File Offset: 0x0000629A
		internal string Version { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000080A2 File Offset: 0x000062A2
		internal string HeaderSource { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000080AA File Offset: 0x000062AA
		internal string NextRequestUri { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000080B2 File Offset: 0x000062B2
		internal string InitialUri { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000080BA File Offset: 0x000062BA
		internal RequestMethod RequestMethod { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000080C2 File Offset: 0x000062C2
		[Nullable(2)]
		internal string LastKnownLocation
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000080CA File Offset: 0x000062CA
		internal string FinalStateVia { get; }

		// Token: 0x060002A3 RID: 675 RVA: 0x000080D4 File Offset: 0x000062D4
		internal RehydrationToken([Nullable(2)] string id, [Nullable(2)] string version, string headerSource, string nextRequestUri, string initialUri, RequestMethod requestMethod, [Nullable(2)] string lastKnownLocation, string finalStateVia)
		{
			this.Version = "1.0.0";
			this.Id = id;
			if (version != null)
			{
				this.Version = version;
			}
			this.HeaderSource = headerSource;
			this.NextRequestUri = nextRequestUri;
			this.InitialUri = initialUri;
			this.RequestMethod = requestMethod;
			this.LastKnownLocation = lastKnownLocation;
			this.FinalStateVia = finalStateVia;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000812C File Offset: 0x0000632C
		internal unsafe RehydrationToken DeserializeRehydrationToken(JsonElement element, ModelReaderWriterOptions options)
		{
			string text = ((options.Format == "W") ? this.GetFormatFromOptions(options) : options.Format);
			if (text != "J")
			{
				throw new FormatException("The model RehydrationToken does not support '" + text + "' format.");
			}
			if (element.ValueKind == 7)
			{
				throw new InvalidOperationException("Cannot deserialize a null value to a non-nullable RehydrationToken");
			}
			string text2 = null;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			RequestMethod requestMethod = default(RequestMethod);
			string text7 = null;
			string text8 = string.Empty;
			foreach (JsonProperty jsonProperty in element.EnumerateObject())
			{
				if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B1F1E1EFA0422888346CB01744E905F325BE96A2431EC34557DDC5EB50EF82F8), 2)))
				{
					if (jsonProperty.Value.ValueKind == 7)
					{
						continue;
					}
					text2 = jsonProperty.Value.GetString();
				}
				if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.51E4F61E075A2E46B61B2ECAC253A0B6CCBF9ACA8D1AFEBCB7E196F49174383B), 7)))
				{
					text3 = jsonProperty.Value.GetString();
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.CB8DD1B50279BAE6BE43B3401F39FD77CC2C0764404126D81268769A854C4323), 12)))
				{
					text4 = jsonProperty.Value.GetString();
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.EDF907CFC4297C83041E1837219CA4FE689B2B756FB177BFA71C4BA140F8BAAC), 14)))
				{
					text5 = jsonProperty.Value.GetString();
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.81D0421A33612BDC0CE15CFDBB43E2AAF39A17A5B401241256FAEA63E9729A0E), 10)))
				{
					text6 = jsonProperty.Value.GetString();
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F3D863D7C13B4AB60952CC94D21B7CEBF5FE08EEB431D55613DDCEA33B583157), 13)))
				{
					string @string = jsonProperty.Value.GetString();
					requestMethod = new RequestMethod(@string);
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.3989A14C38D8A5311F6EF6549FE830D216F2BD3E986B66B3113B805E1BD499EE), 17)))
				{
					if (jsonProperty.Value.ValueKind != 7)
					{
						text7 = jsonProperty.Value.GetString();
					}
				}
				else if (jsonProperty.NameEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.CB6EF4A19F1C90ACFF8A02070F53E01F8B857D39943AA4FD99079ED01C6043DC), 13)))
				{
					text8 = jsonProperty.Value.GetString();
				}
			}
			return new RehydrationToken(text2, text3, text4, text5, text6, requestMethod, text7, text8);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000083B4 File Offset: 0x000065B4
		unsafe void IJsonModel<RehydrationToken>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B1F1E1EFA0422888346CB01744E905F325BE96A2431EC34557DDC5EB50EF82F8), 2));
			writer.WriteStringValue(this.Id);
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.51E4F61E075A2E46B61B2ECAC253A0B6CCBF9ACA8D1AFEBCB7E196F49174383B), 7));
			writer.WriteStringValue(this.Version);
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.CB8DD1B50279BAE6BE43B3401F39FD77CC2C0764404126D81268769A854C4323), 12));
			writer.WriteStringValue(this.HeaderSource.ToString());
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.EDF907CFC4297C83041E1837219CA4FE689B2B756FB177BFA71C4BA140F8BAAC), 14));
			writer.WriteStringValue(this.NextRequestUri);
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.81D0421A33612BDC0CE15CFDBB43E2AAF39A17A5B401241256FAEA63E9729A0E), 10));
			writer.WriteStringValue(this.InitialUri);
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F3D863D7C13B4AB60952CC94D21B7CEBF5FE08EEB431D55613DDCEA33B583157), 13));
			writer.WriteStringValue(this.RequestMethod.ToString());
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.3989A14C38D8A5311F6EF6549FE830D216F2BD3E986B66B3113B805E1BD499EE), 17));
			writer.WriteStringValue(this.LastKnownLocation);
			writer.WritePropertyName(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.CB6EF4A19F1C90ACFF8A02070F53E01F8B857D39943AA4FD99079ED01C6043DC), 13));
			writer.WriteStringValue(this.FinalStateVia.ToString());
			writer.WriteEndObject();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000084D4 File Offset: 0x000066D4
		RehydrationToken IJsonModel<RehydrationToken>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
		{
			string text = ((options.Format == "W") ? this.GetFormatFromOptions(options) : options.Format);
			if (text != "J")
			{
				throw new FormatException("The model RehydrationToken does not support '" + text + "' format.");
			}
			RehydrationToken rehydrationToken;
			using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
			{
				rehydrationToken = this.DeserializeRehydrationToken(jsonDocument.RootElement, options);
			}
			return rehydrationToken;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008564 File Offset: 0x00006764
		BinaryData IPersistableModel<RehydrationToken>.Write(ModelReaderWriterOptions options)
		{
			if (((options.Format == "W") ? this.GetFormatFromOptions(options) : options.Format) == "J")
			{
				return ModelReaderWriter.Write<RehydrationToken>(this, options);
			}
			throw new FormatException("The model RehydrationToken does not support '" + options.Format + "' format.");
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000085D0 File Offset: 0x000067D0
		RehydrationToken IPersistableModel<RehydrationToken>.Create(BinaryData data, ModelReaderWriterOptions options)
		{
			if (((options.Format == "W") ? this.GetFormatFromOptions(options) : options.Format) == "J")
			{
				using (JsonDocument jsonDocument = JsonDocument.Parse(data, default(JsonDocumentOptions)))
				{
					return this.DeserializeRehydrationToken(jsonDocument.RootElement, options);
				}
			}
			throw new FormatException("The model RehydrationToken does not support '" + options.Format + "' format.");
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008670 File Offset: 0x00006870
		string IPersistableModel<RehydrationToken>.GetFormatFromOptions(ModelReaderWriterOptions options)
		{
			return "J";
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008677 File Offset: 0x00006877
		BinaryData IPersistableModel<object>.Write(ModelReaderWriterOptions options)
		{
			return this.Write(options);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000868A File Offset: 0x0000688A
		object IPersistableModel<object>.Create(BinaryData data, ModelReaderWriterOptions options)
		{
			return this.Create(data, options);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000086A3 File Offset: 0x000068A3
		string IPersistableModel<object>.GetFormatFromOptions(ModelReaderWriterOptions options)
		{
			return "J";
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000086AA File Offset: 0x000068AA
		void IJsonModel<object>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
		{
			this.Write(writer, options);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000086BE File Offset: 0x000068BE
		object IJsonModel<object>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
		{
			return this.Create(ref reader, options);
		}
	}
}
