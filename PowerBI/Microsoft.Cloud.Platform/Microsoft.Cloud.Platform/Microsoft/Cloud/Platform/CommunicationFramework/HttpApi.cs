using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.CommunicationFramework
{
	// Token: 0x02000472 RID: 1138
	internal static class HttpApi
	{
		// Token: 0x0600237B RID: 9083 RVA: 0x000805F0 File Offset: 0x0007E7F0
		internal unsafe static HttpApi.HTTP_SSL_PROTOCOL_INFO GetSslProtocolInfo(byte[] memoryBlob, IntPtr originalAddress)
		{
			fixed (byte[] array = memoryBlob)
			{
				byte* ptr;
				if (memoryBlob == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				HttpApi.HTTP_REQUEST_V2* ptr2 = (HttpApi.HTTP_REQUEST_V2*)ptr;
				long num = (long)((byte*)ptr - (byte*)(void*)originalAddress);
				for (int i = 0; i < (int)ptr2->RequestInfoCount; i++)
				{
					HttpApi.HTTP_REQUEST_INFO* ptr3 = num + (ptr2->pRequestInfo + i) / sizeof(HttpApi.HTTP_REQUEST_INFO);
					if (ptr3 != null && ptr3->InfoType == HttpApi.HTTP_REQUEST_INFO_TYPE.HttpRequestInfoTypeSslProtocol)
					{
						return Marshal.PtrToStructure<HttpApi.HTTP_SSL_PROTOCOL_INFO>(new IntPtr(ptr3->pInfo));
					}
				}
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00080678 File Offset: 0x0007E878
		internal unsafe static HttpApi.HTTP_REQUEST_AUTH_INFO GetAuthInfo(byte[] memoryBlob, IntPtr originalAddress)
		{
			fixed (byte[] array = memoryBlob)
			{
				byte* ptr;
				if (memoryBlob == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				HttpApi.HTTP_REQUEST_V2* ptr2 = (HttpApi.HTTP_REQUEST_V2*)ptr;
				long num = (long)((byte*)ptr - (byte*)(void*)originalAddress);
				for (int i = 0; i < (int)ptr2->RequestInfoCount; i++)
				{
					HttpApi.HTTP_REQUEST_INFO* ptr3 = num + (ptr2->pRequestInfo + i) / sizeof(HttpApi.HTTP_REQUEST_INFO);
					if (ptr3 != null && ptr3->InfoType == HttpApi.HTTP_REQUEST_INFO_TYPE.HttpRequestInfoTypeAuth)
					{
						return Marshal.PtrToStructure<HttpApi.HTTP_REQUEST_AUTH_INFO>(new IntPtr(ptr3->pInfo));
					}
				}
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x02000819 RID: 2073
		internal enum HTTP_REQUEST_INFO_TYPE
		{
			// Token: 0x04001841 RID: 6209
			HttpRequestInfoTypeAuth,
			// Token: 0x04001842 RID: 6210
			HttpRequestInfoTypeChannelBind,
			// Token: 0x04001843 RID: 6211
			HttpRequestInfoTypeSslProtocol,
			// Token: 0x04001844 RID: 6212
			HttpRequestInfoTypeSslTokenBindingDraft,
			// Token: 0x04001845 RID: 6213
			HttpRequestInfoTypeSslTokenBinding
		}

		// Token: 0x0200081A RID: 2074
		internal struct HTTP_REQUEST_INFO
		{
			// Token: 0x04001846 RID: 6214
			internal HttpApi.HTTP_REQUEST_INFO_TYPE InfoType;

			// Token: 0x04001847 RID: 6215
			internal uint InfoLength;

			// Token: 0x04001848 RID: 6216
			internal unsafe void* pInfo;
		}

		// Token: 0x0200081B RID: 2075
		internal struct HTTP_VERSION
		{
			// Token: 0x04001849 RID: 6217
			internal ushort MajorVersion;

			// Token: 0x0400184A RID: 6218
			internal ushort MinorVersion;
		}

		// Token: 0x0200081C RID: 2076
		internal enum HTTP_VERB
		{
			// Token: 0x0400184C RID: 6220
			HttpVerbUnparsed,
			// Token: 0x0400184D RID: 6221
			HttpVerbUnknown,
			// Token: 0x0400184E RID: 6222
			HttpVerbInvalid,
			// Token: 0x0400184F RID: 6223
			HttpVerbOPTIONS,
			// Token: 0x04001850 RID: 6224
			HttpVerbGET,
			// Token: 0x04001851 RID: 6225
			HttpVerbHEAD,
			// Token: 0x04001852 RID: 6226
			HttpVerbPOST,
			// Token: 0x04001853 RID: 6227
			HttpVerbPUT,
			// Token: 0x04001854 RID: 6228
			HttpVerbDELETE,
			// Token: 0x04001855 RID: 6229
			HttpVerbTRACE,
			// Token: 0x04001856 RID: 6230
			HttpVerbCONNECT,
			// Token: 0x04001857 RID: 6231
			HttpVerbTRACK,
			// Token: 0x04001858 RID: 6232
			HttpVerbMOVE,
			// Token: 0x04001859 RID: 6233
			HttpVerbCOPY,
			// Token: 0x0400185A RID: 6234
			HttpVerbPROPFIND,
			// Token: 0x0400185B RID: 6235
			HttpVerbPROPPATCH,
			// Token: 0x0400185C RID: 6236
			HttpVerbMKCOL,
			// Token: 0x0400185D RID: 6237
			HttpVerbLOCK,
			// Token: 0x0400185E RID: 6238
			HttpVerbUNLOCK,
			// Token: 0x0400185F RID: 6239
			HttpVerbSEARCH,
			// Token: 0x04001860 RID: 6240
			HttpVerbMaximum
		}

		// Token: 0x0200081D RID: 2077
		internal struct HTTP_COOKED_URL
		{
			// Token: 0x04001861 RID: 6241
			internal ushort FullUrlLength;

			// Token: 0x04001862 RID: 6242
			internal ushort HostLength;

			// Token: 0x04001863 RID: 6243
			internal ushort AbsPathLength;

			// Token: 0x04001864 RID: 6244
			internal ushort QueryStringLength;

			// Token: 0x04001865 RID: 6245
			internal unsafe ushort* pFullUrl;

			// Token: 0x04001866 RID: 6246
			internal unsafe ushort* pHost;

			// Token: 0x04001867 RID: 6247
			internal unsafe ushort* pAbsPath;

			// Token: 0x04001868 RID: 6248
			internal unsafe ushort* pQueryString;
		}

		// Token: 0x0200081E RID: 2078
		internal struct SOCKADDR
		{
			// Token: 0x04001869 RID: 6249
			internal ushort sa_family;

			// Token: 0x0400186A RID: 6250
			internal byte sa_data;

			// Token: 0x0400186B RID: 6251
			internal byte sa_data_02;

			// Token: 0x0400186C RID: 6252
			internal byte sa_data_03;

			// Token: 0x0400186D RID: 6253
			internal byte sa_data_04;

			// Token: 0x0400186E RID: 6254
			internal byte sa_data_05;

			// Token: 0x0400186F RID: 6255
			internal byte sa_data_06;

			// Token: 0x04001870 RID: 6256
			internal byte sa_data_07;

			// Token: 0x04001871 RID: 6257
			internal byte sa_data_08;

			// Token: 0x04001872 RID: 6258
			internal byte sa_data_09;

			// Token: 0x04001873 RID: 6259
			internal byte sa_data_10;

			// Token: 0x04001874 RID: 6260
			internal byte sa_data_11;

			// Token: 0x04001875 RID: 6261
			internal byte sa_data_12;

			// Token: 0x04001876 RID: 6262
			internal byte sa_data_13;

			// Token: 0x04001877 RID: 6263
			internal byte sa_data_14;
		}

		// Token: 0x0200081F RID: 2079
		internal struct HTTP_TRANSPORT_ADDRESS
		{
			// Token: 0x04001878 RID: 6264
			internal unsafe HttpApi.SOCKADDR* pRemoteAddress;

			// Token: 0x04001879 RID: 6265
			internal unsafe HttpApi.SOCKADDR* pLocalAddress;
		}

		// Token: 0x02000820 RID: 2080
		internal struct HTTP_UNKNOWN_HEADER
		{
			// Token: 0x0400187A RID: 6266
			internal ushort NameLength;

			// Token: 0x0400187B RID: 6267
			internal ushort RawValueLength;

			// Token: 0x0400187C RID: 6268
			internal unsafe sbyte* pName;

			// Token: 0x0400187D RID: 6269
			internal unsafe sbyte* pRawValue;
		}

		// Token: 0x02000821 RID: 2081
		internal struct HTTP_KNOWN_HEADER
		{
			// Token: 0x0400187E RID: 6270
			internal ushort RawValueLength;

			// Token: 0x0400187F RID: 6271
			internal unsafe sbyte* pRawValue;
		}

		// Token: 0x02000822 RID: 2082
		internal struct HTTP_REQUEST_HEADERS
		{
			// Token: 0x04001880 RID: 6272
			internal ushort UnknownHeaderCount;

			// Token: 0x04001881 RID: 6273
			internal unsafe HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

			// Token: 0x04001882 RID: 6274
			internal ushort TrailerCount;

			// Token: 0x04001883 RID: 6275
			internal unsafe HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

			// Token: 0x04001884 RID: 6276
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

			// Token: 0x04001885 RID: 6277
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

			// Token: 0x04001886 RID: 6278
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

			// Token: 0x04001887 RID: 6279
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

			// Token: 0x04001888 RID: 6280
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

			// Token: 0x04001889 RID: 6281
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

			// Token: 0x0400188A RID: 6282
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

			// Token: 0x0400188B RID: 6283
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

			// Token: 0x0400188C RID: 6284
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

			// Token: 0x0400188D RID: 6285
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

			// Token: 0x0400188E RID: 6286
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

			// Token: 0x0400188F RID: 6287
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

			// Token: 0x04001890 RID: 6288
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

			// Token: 0x04001891 RID: 6289
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

			// Token: 0x04001892 RID: 6290
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

			// Token: 0x04001893 RID: 6291
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

			// Token: 0x04001894 RID: 6292
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

			// Token: 0x04001895 RID: 6293
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

			// Token: 0x04001896 RID: 6294
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

			// Token: 0x04001897 RID: 6295
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

			// Token: 0x04001898 RID: 6296
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

			// Token: 0x04001899 RID: 6297
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

			// Token: 0x0400189A RID: 6298
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

			// Token: 0x0400189B RID: 6299
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

			// Token: 0x0400189C RID: 6300
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

			// Token: 0x0400189D RID: 6301
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

			// Token: 0x0400189E RID: 6302
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

			// Token: 0x0400189F RID: 6303
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

			// Token: 0x040018A0 RID: 6304
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

			// Token: 0x040018A1 RID: 6305
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;

			// Token: 0x040018A2 RID: 6306
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_31;

			// Token: 0x040018A3 RID: 6307
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_32;

			// Token: 0x040018A4 RID: 6308
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_33;

			// Token: 0x040018A5 RID: 6309
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_34;

			// Token: 0x040018A6 RID: 6310
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_35;

			// Token: 0x040018A7 RID: 6311
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_36;

			// Token: 0x040018A8 RID: 6312
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_37;

			// Token: 0x040018A9 RID: 6313
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_38;

			// Token: 0x040018AA RID: 6314
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_39;

			// Token: 0x040018AB RID: 6315
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_40;

			// Token: 0x040018AC RID: 6316
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_41;
		}

		// Token: 0x02000823 RID: 2083
		internal enum HTTP_DATA_CHUNK_TYPE
		{
			// Token: 0x040018AE RID: 6318
			HttpDataChunkFromMemory,
			// Token: 0x040018AF RID: 6319
			HttpDataChunkFromFileHandle,
			// Token: 0x040018B0 RID: 6320
			HttpDataChunkFromFragmentCache,
			// Token: 0x040018B1 RID: 6321
			HttpDataChunkMaximum
		}

		// Token: 0x02000824 RID: 2084
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		internal struct HTTP_DATA_CHUNK
		{
			// Token: 0x040018B2 RID: 6322
			internal HttpApi.HTTP_DATA_CHUNK_TYPE DataChunkType;

			// Token: 0x040018B3 RID: 6323
			internal uint p0;

			// Token: 0x040018B4 RID: 6324
			internal unsafe byte* pBuffer;

			// Token: 0x040018B5 RID: 6325
			internal uint BufferLength;
		}

		// Token: 0x02000825 RID: 2085
		internal struct HTTP_SSL_CLIENT_CERT_INFO
		{
			// Token: 0x040018B6 RID: 6326
			internal uint CertFlags;

			// Token: 0x040018B7 RID: 6327
			internal uint CertEncodedSize;

			// Token: 0x040018B8 RID: 6328
			internal unsafe byte* pCertEncoded;

			// Token: 0x040018B9 RID: 6329
			internal unsafe void* Token;

			// Token: 0x040018BA RID: 6330
			internal byte CertDeniedByMapper;
		}

		// Token: 0x02000826 RID: 2086
		internal struct HTTP_SSL_INFO
		{
			// Token: 0x040018BB RID: 6331
			internal ushort ServerCertKeySize;

			// Token: 0x040018BC RID: 6332
			internal ushort ConnectionKeySize;

			// Token: 0x040018BD RID: 6333
			internal uint ServerCertIssuerSize;

			// Token: 0x040018BE RID: 6334
			internal uint ServerCertSubjectSize;

			// Token: 0x040018BF RID: 6335
			internal unsafe sbyte* pServerCertIssuer;

			// Token: 0x040018C0 RID: 6336
			internal unsafe sbyte* pServerCertSubject;

			// Token: 0x040018C1 RID: 6337
			internal unsafe HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pClientCertInfo;

			// Token: 0x040018C2 RID: 6338
			internal uint SslClientCertNegotiated;
		}

		// Token: 0x02000827 RID: 2087
		internal struct HTTP_REQUEST
		{
			// Token: 0x040018C3 RID: 6339
			internal uint Flags;

			// Token: 0x040018C4 RID: 6340
			internal ulong ConnectionId;

			// Token: 0x040018C5 RID: 6341
			internal ulong RequestId;

			// Token: 0x040018C6 RID: 6342
			internal ulong UrlContext;

			// Token: 0x040018C7 RID: 6343
			internal HttpApi.HTTP_VERSION Version;

			// Token: 0x040018C8 RID: 6344
			internal HttpApi.HTTP_VERB Verb;

			// Token: 0x040018C9 RID: 6345
			internal ushort UnknownVerbLength;

			// Token: 0x040018CA RID: 6346
			internal ushort RawUrlLength;

			// Token: 0x040018CB RID: 6347
			internal unsafe sbyte* pUnknownVerb;

			// Token: 0x040018CC RID: 6348
			internal unsafe sbyte* pRawUrl;

			// Token: 0x040018CD RID: 6349
			internal HttpApi.HTTP_COOKED_URL CookedUrl;

			// Token: 0x040018CE RID: 6350
			internal HttpApi.HTTP_TRANSPORT_ADDRESS Address;

			// Token: 0x040018CF RID: 6351
			internal HttpApi.HTTP_REQUEST_HEADERS Headers;

			// Token: 0x040018D0 RID: 6352
			internal ulong BytesReceived;

			// Token: 0x040018D1 RID: 6353
			internal ushort EntityChunkCount;

			// Token: 0x040018D2 RID: 6354
			internal unsafe HttpApi.HTTP_DATA_CHUNK* pEntityChunks;

			// Token: 0x040018D3 RID: 6355
			internal ulong RawConnectionId;

			// Token: 0x040018D4 RID: 6356
			internal unsafe HttpApi.HTTP_SSL_INFO* pSslInfo;
		}

		// Token: 0x02000828 RID: 2088
		internal struct HTTP_REQUEST_V2
		{
			// Token: 0x040018D5 RID: 6357
			internal HttpApi.HTTP_REQUEST RequestV1;

			// Token: 0x040018D6 RID: 6358
			internal ushort RequestInfoCount;

			// Token: 0x040018D7 RID: 6359
			internal unsafe HttpApi.HTTP_REQUEST_INFO* pRequestInfo;
		}

		// Token: 0x02000829 RID: 2089
		internal struct HTTP_SSL_PROTOCOL_INFO
		{
			// Token: 0x060032C1 RID: 12993 RVA: 0x000AA098 File Offset: 0x000A8298
			public string ProtocolName()
			{
				uint protocol = this.Protocol;
				if (protocol <= 32U)
				{
					if (protocol <= 4U)
					{
						if (protocol - 1U <= 1U)
						{
							return "PCT 1.0";
						}
						if (protocol != 4U)
						{
							goto IL_0089;
						}
					}
					else if (protocol != 8U)
					{
						if (protocol != 16U && protocol != 32U)
						{
							goto IL_0089;
						}
						return "SSL 3.0";
					}
					return "PCT 2.0";
				}
				if (protocol <= 256U)
				{
					if (protocol == 64U || protocol == 128U)
					{
						return "TLS 1.0";
					}
					if (protocol != 256U)
					{
						goto IL_0089;
					}
				}
				else if (protocol != 512U)
				{
					if (protocol != 1024U && protocol != 2048U)
					{
						goto IL_0089;
					}
					return "TLS 1.2";
				}
				return "TLS 1.1";
				IL_0089:
				return string.Format("UNK:{0:X}", this.Protocol);
			}

			// Token: 0x060032C2 RID: 12994 RVA: 0x000AA144 File Offset: 0x000A8344
			public string AlgName(uint algId)
			{
				if (algId <= 32772U)
				{
					if (algId <= 26115U)
					{
						if (algId == 0U)
						{
							return "(null)";
						}
						switch (algId)
						{
						case 26113U:
							return "DES";
						case 26114U:
							return "RC2";
						case 26115U:
							return "3DES";
						}
					}
					else
					{
						switch (algId)
						{
						case 26126U:
							return "AES128";
						case 26127U:
							return "AES192";
						case 26128U:
							return "AES256";
						case 26129U:
							return "AES";
						default:
							if (algId == 26625U)
							{
								return "RC4";
							}
							switch (algId)
							{
							case 32769U:
								return "MD2";
							case 32770U:
								return "MD4";
							case 32771U:
								return "MD5";
							case 32772U:
								return "SHA1";
							}
							break;
						}
					}
				}
				else if (algId <= 40961U)
				{
					switch (algId)
					{
					case 32780U:
						return "SHA256";
					case 32781U:
						return "SHA384";
					case 32782U:
						return "SHA512";
					default:
						if (algId == 40961U)
						{
							return "ECMQV";
						}
						break;
					}
				}
				else
				{
					if (algId == 41984U)
					{
						return "RSA";
					}
					switch (algId)
					{
					case 43521U:
						return "DH";
					case 43522U:
						return "DHE";
					case 43523U:
					case 43524U:
						break;
					case 43525U:
						return "ECDH";
					default:
						if (algId == 44550U)
						{
							return "ECDHE";
						}
						break;
					}
				}
				return string.Format("UNK:{0:X}", algId);
			}

			// Token: 0x040018D8 RID: 6360
			public uint Protocol;

			// Token: 0x040018D9 RID: 6361
			public uint CipherType;

			// Token: 0x040018DA RID: 6362
			public uint CipherStrength;

			// Token: 0x040018DB RID: 6363
			public uint HashType;

			// Token: 0x040018DC RID: 6364
			public uint HashStrength;

			// Token: 0x040018DD RID: 6365
			public uint KeyExchangeType;

			// Token: 0x040018DE RID: 6366
			public uint KeyExchangeStrength;
		}

		// Token: 0x0200082A RID: 2090
		internal struct HTTP_REQUEST_AUTH_INFO
		{
			// Token: 0x040018DF RID: 6367
			public uint AuthStatus;

			// Token: 0x040018E0 RID: 6368
			public uint SecStatus;

			// Token: 0x040018E1 RID: 6369
			public uint Flags;

			// Token: 0x040018E2 RID: 6370
			public uint AuthType;

			// Token: 0x040018E3 RID: 6371
			public IntPtr AccessToken;

			// Token: 0x040018E4 RID: 6372
			public uint ContextAttributes;

			// Token: 0x040018E5 RID: 6373
			public uint PacketContextLength;

			// Token: 0x040018E6 RID: 6374
			public uint PackedContextType;

			// Token: 0x040018E7 RID: 6375
			public IntPtr PackedContext;

			// Token: 0x040018E8 RID: 6376
			public uint MutualAuthDataLength;

			// Token: 0x040018E9 RID: 6377
			public IntPtr pMutualAuthData;
		}
	}
}
