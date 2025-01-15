using System;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Common
{
	// Token: 0x020000D8 RID: 216
	internal static class HttpApi
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001422C File Offset: 0x0001242C
		internal unsafe static HttpApi.HTTP_SSL_PROTOCOL_INFO GetTlsProtocolInfo(byte[] memoryBlob, IntPtr originalAddress)
		{
			if (memoryBlob != null)
			{
				try
				{
					try
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
								if (ptr3 != null && ptr3->pInfo != null && ptr3->InfoType == HttpApi.HTTP_REQUEST_INFO_TYPE.HttpRequestInfoTypeSslProtocol)
								{
									return num[ptr3->pInfo / sizeof(HttpApi.HTTP_SSL_PROTOCOL_INFO)];
								}
							}
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
				catch (Exception ex)
				{
					EngineTracer.Warn(string.Format("Error occcured while getting the SSL protocol information from the request : {0}", ex));
				}
			}
			return default(HttpApi.HTTP_SSL_PROTOCOL_INFO);
		}

		// Token: 0x0200010D RID: 269
		internal enum HTTP_REQUEST_INFO_TYPE
		{
			// Token: 0x04000567 RID: 1383
			HttpRequestInfoTypeAuth,
			// Token: 0x04000568 RID: 1384
			HttpRequestInfoTypeChannelBind,
			// Token: 0x04000569 RID: 1385
			HttpRequestInfoTypeSslProtocol,
			// Token: 0x0400056A RID: 1386
			HttpRequestInfoTypeSslTokenBindingDraft,
			// Token: 0x0400056B RID: 1387
			HttpRequestInfoTypeSslTokenBinding
		}

		// Token: 0x0200010E RID: 270
		internal enum HTTP_VERB
		{
			// Token: 0x0400056D RID: 1389
			HttpVerbUnparsed,
			// Token: 0x0400056E RID: 1390
			HttpVerbUnknown,
			// Token: 0x0400056F RID: 1391
			HttpVerbInvalid,
			// Token: 0x04000570 RID: 1392
			HttpVerbOPTIONS,
			// Token: 0x04000571 RID: 1393
			HttpVerbGET,
			// Token: 0x04000572 RID: 1394
			HttpVerbHEAD,
			// Token: 0x04000573 RID: 1395
			HttpVerbPOST,
			// Token: 0x04000574 RID: 1396
			HttpVerbPUT,
			// Token: 0x04000575 RID: 1397
			HttpVerbDELETE,
			// Token: 0x04000576 RID: 1398
			HttpVerbTRACE,
			// Token: 0x04000577 RID: 1399
			HttpVerbCONNECT,
			// Token: 0x04000578 RID: 1400
			HttpVerbTRACK,
			// Token: 0x04000579 RID: 1401
			HttpVerbMOVE,
			// Token: 0x0400057A RID: 1402
			HttpVerbCOPY,
			// Token: 0x0400057B RID: 1403
			HttpVerbPROPFIND,
			// Token: 0x0400057C RID: 1404
			HttpVerbPROPPATCH,
			// Token: 0x0400057D RID: 1405
			HttpVerbMKCOL,
			// Token: 0x0400057E RID: 1406
			HttpVerbLOCK,
			// Token: 0x0400057F RID: 1407
			HttpVerbUNLOCK,
			// Token: 0x04000580 RID: 1408
			HttpVerbSEARCH,
			// Token: 0x04000581 RID: 1409
			HttpVerbMaximum
		}

		// Token: 0x0200010F RID: 271
		internal enum HTTP_DATA_CHUNK_TYPE
		{
			// Token: 0x04000583 RID: 1411
			HttpDataChunkFromMemory,
			// Token: 0x04000584 RID: 1412
			HttpDataChunkFromFileHandle,
			// Token: 0x04000585 RID: 1413
			HttpDataChunkFromFragmentCache,
			// Token: 0x04000586 RID: 1414
			HttpDataChunkMaximum
		}

		// Token: 0x02000110 RID: 272
		private struct HTTP_REQUEST_INFO
		{
			// Token: 0x04000587 RID: 1415
			internal HttpApi.HTTP_REQUEST_INFO_TYPE InfoType;

			// Token: 0x04000588 RID: 1416
			internal uint InfoLength;

			// Token: 0x04000589 RID: 1417
			internal unsafe void* pInfo;
		}

		// Token: 0x02000111 RID: 273
		private struct HTTP_VERSION
		{
			// Token: 0x0400058A RID: 1418
			internal ushort MajorVersion;

			// Token: 0x0400058B RID: 1419
			internal ushort MinorVersion;
		}

		// Token: 0x02000112 RID: 274
		private struct HTTP_COOKED_URL
		{
			// Token: 0x0400058C RID: 1420
			internal ushort FullUrlLength;

			// Token: 0x0400058D RID: 1421
			internal ushort HostLength;

			// Token: 0x0400058E RID: 1422
			internal ushort AbsPathLength;

			// Token: 0x0400058F RID: 1423
			internal ushort QueryStringLength;

			// Token: 0x04000590 RID: 1424
			internal unsafe ushort* pFullUrl;

			// Token: 0x04000591 RID: 1425
			internal unsafe ushort* pHost;

			// Token: 0x04000592 RID: 1426
			internal unsafe ushort* pAbsPath;

			// Token: 0x04000593 RID: 1427
			internal unsafe ushort* pQueryString;
		}

		// Token: 0x02000113 RID: 275
		private struct SOCKADDR
		{
			// Token: 0x04000594 RID: 1428
			internal ushort sa_family;

			// Token: 0x04000595 RID: 1429
			internal byte sa_data;

			// Token: 0x04000596 RID: 1430
			internal byte sa_data_02;

			// Token: 0x04000597 RID: 1431
			internal byte sa_data_03;

			// Token: 0x04000598 RID: 1432
			internal byte sa_data_04;

			// Token: 0x04000599 RID: 1433
			internal byte sa_data_05;

			// Token: 0x0400059A RID: 1434
			internal byte sa_data_06;

			// Token: 0x0400059B RID: 1435
			internal byte sa_data_07;

			// Token: 0x0400059C RID: 1436
			internal byte sa_data_08;

			// Token: 0x0400059D RID: 1437
			internal byte sa_data_09;

			// Token: 0x0400059E RID: 1438
			internal byte sa_data_10;

			// Token: 0x0400059F RID: 1439
			internal byte sa_data_11;

			// Token: 0x040005A0 RID: 1440
			internal byte sa_data_12;

			// Token: 0x040005A1 RID: 1441
			internal byte sa_data_13;

			// Token: 0x040005A2 RID: 1442
			internal byte sa_data_14;
		}

		// Token: 0x02000114 RID: 276
		private struct HTTP_TRANSPORT_ADDRESS
		{
			// Token: 0x040005A3 RID: 1443
			internal unsafe HttpApi.SOCKADDR* pRemoteAddress;

			// Token: 0x040005A4 RID: 1444
			internal unsafe HttpApi.SOCKADDR* pLocalAddress;
		}

		// Token: 0x02000115 RID: 277
		private struct HTTP_UNKNOWN_HEADER
		{
			// Token: 0x040005A5 RID: 1445
			internal ushort NameLength;

			// Token: 0x040005A6 RID: 1446
			internal ushort RawValueLength;

			// Token: 0x040005A7 RID: 1447
			internal unsafe sbyte* pName;

			// Token: 0x040005A8 RID: 1448
			internal unsafe sbyte* pRawValue;
		}

		// Token: 0x02000116 RID: 278
		private struct HTTP_KNOWN_HEADER
		{
			// Token: 0x040005A9 RID: 1449
			internal ushort RawValueLength;

			// Token: 0x040005AA RID: 1450
			internal unsafe sbyte* pRawValue;
		}

		// Token: 0x02000117 RID: 279
		private struct HTTP_REQUEST_HEADERS
		{
			// Token: 0x040005AB RID: 1451
			internal ushort UnknownHeaderCount;

			// Token: 0x040005AC RID: 1452
			internal unsafe HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

			// Token: 0x040005AD RID: 1453
			internal ushort TrailerCount;

			// Token: 0x040005AE RID: 1454
			internal unsafe HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

			// Token: 0x040005AF RID: 1455
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

			// Token: 0x040005B0 RID: 1456
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

			// Token: 0x040005B1 RID: 1457
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

			// Token: 0x040005B2 RID: 1458
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

			// Token: 0x040005B3 RID: 1459
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

			// Token: 0x040005B4 RID: 1460
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

			// Token: 0x040005B5 RID: 1461
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

			// Token: 0x040005B6 RID: 1462
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

			// Token: 0x040005B7 RID: 1463
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

			// Token: 0x040005B8 RID: 1464
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

			// Token: 0x040005B9 RID: 1465
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

			// Token: 0x040005BA RID: 1466
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

			// Token: 0x040005BB RID: 1467
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

			// Token: 0x040005BC RID: 1468
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

			// Token: 0x040005BD RID: 1469
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

			// Token: 0x040005BE RID: 1470
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

			// Token: 0x040005BF RID: 1471
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

			// Token: 0x040005C0 RID: 1472
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

			// Token: 0x040005C1 RID: 1473
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

			// Token: 0x040005C2 RID: 1474
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

			// Token: 0x040005C3 RID: 1475
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

			// Token: 0x040005C4 RID: 1476
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

			// Token: 0x040005C5 RID: 1477
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

			// Token: 0x040005C6 RID: 1478
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

			// Token: 0x040005C7 RID: 1479
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

			// Token: 0x040005C8 RID: 1480
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

			// Token: 0x040005C9 RID: 1481
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

			// Token: 0x040005CA RID: 1482
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

			// Token: 0x040005CB RID: 1483
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

			// Token: 0x040005CC RID: 1484
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;

			// Token: 0x040005CD RID: 1485
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_31;

			// Token: 0x040005CE RID: 1486
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_32;

			// Token: 0x040005CF RID: 1487
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_33;

			// Token: 0x040005D0 RID: 1488
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_34;

			// Token: 0x040005D1 RID: 1489
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_35;

			// Token: 0x040005D2 RID: 1490
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_36;

			// Token: 0x040005D3 RID: 1491
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_37;

			// Token: 0x040005D4 RID: 1492
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_38;

			// Token: 0x040005D5 RID: 1493
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_39;

			// Token: 0x040005D6 RID: 1494
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_40;

			// Token: 0x040005D7 RID: 1495
			internal HttpApi.HTTP_KNOWN_HEADER KnownHeaders_41;
		}

		// Token: 0x02000118 RID: 280
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		private struct HTTP_DATA_CHUNK
		{
			// Token: 0x040005D8 RID: 1496
			internal HttpApi.HTTP_DATA_CHUNK_TYPE DataChunkType;

			// Token: 0x040005D9 RID: 1497
			internal uint p0;

			// Token: 0x040005DA RID: 1498
			internal unsafe byte* pBuffer;

			// Token: 0x040005DB RID: 1499
			internal uint BufferLength;
		}

		// Token: 0x02000119 RID: 281
		private struct HTTP_SSL_CLIENT_CERT_INFO
		{
			// Token: 0x040005DC RID: 1500
			internal uint CertFlags;

			// Token: 0x040005DD RID: 1501
			internal uint CertEncodedSize;

			// Token: 0x040005DE RID: 1502
			internal unsafe byte* pCertEncoded;

			// Token: 0x040005DF RID: 1503
			internal unsafe void* Token;

			// Token: 0x040005E0 RID: 1504
			internal byte CertDeniedByMapper;
		}

		// Token: 0x0200011A RID: 282
		private struct HTTP_SSL_INFO
		{
			// Token: 0x040005E1 RID: 1505
			internal ushort ServerCertKeySize;

			// Token: 0x040005E2 RID: 1506
			internal ushort ConnectionKeySize;

			// Token: 0x040005E3 RID: 1507
			internal uint ServerCertIssuerSize;

			// Token: 0x040005E4 RID: 1508
			internal uint ServerCertSubjectSize;

			// Token: 0x040005E5 RID: 1509
			internal unsafe sbyte* pServerCertIssuer;

			// Token: 0x040005E6 RID: 1510
			internal unsafe sbyte* pServerCertSubject;

			// Token: 0x040005E7 RID: 1511
			internal unsafe HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pClientCertInfo;

			// Token: 0x040005E8 RID: 1512
			internal uint SslClientCertNegotiated;
		}

		// Token: 0x0200011B RID: 283
		private struct HTTP_REQUEST
		{
			// Token: 0x040005E9 RID: 1513
			internal uint Flags;

			// Token: 0x040005EA RID: 1514
			internal ulong ConnectionId;

			// Token: 0x040005EB RID: 1515
			internal ulong RequestId;

			// Token: 0x040005EC RID: 1516
			internal ulong UrlContext;

			// Token: 0x040005ED RID: 1517
			internal HttpApi.HTTP_VERSION Version;

			// Token: 0x040005EE RID: 1518
			internal HttpApi.HTTP_VERB Verb;

			// Token: 0x040005EF RID: 1519
			internal ushort UnknownVerbLength;

			// Token: 0x040005F0 RID: 1520
			internal ushort RawUrlLength;

			// Token: 0x040005F1 RID: 1521
			internal unsafe sbyte* pUnknownVerb;

			// Token: 0x040005F2 RID: 1522
			internal unsafe sbyte* pRawUrl;

			// Token: 0x040005F3 RID: 1523
			internal HttpApi.HTTP_COOKED_URL CookedUrl;

			// Token: 0x040005F4 RID: 1524
			internal HttpApi.HTTP_TRANSPORT_ADDRESS Address;

			// Token: 0x040005F5 RID: 1525
			internal HttpApi.HTTP_REQUEST_HEADERS Headers;

			// Token: 0x040005F6 RID: 1526
			internal ulong BytesReceived;

			// Token: 0x040005F7 RID: 1527
			internal ushort EntityChunkCount;

			// Token: 0x040005F8 RID: 1528
			internal unsafe HttpApi.HTTP_DATA_CHUNK* pEntityChunks;

			// Token: 0x040005F9 RID: 1529
			internal ulong RawConnectionId;

			// Token: 0x040005FA RID: 1530
			internal unsafe HttpApi.HTTP_SSL_INFO* pSslInfo;
		}

		// Token: 0x0200011C RID: 284
		private struct HTTP_REQUEST_V2
		{
			// Token: 0x040005FB RID: 1531
			internal HttpApi.HTTP_REQUEST RequestV1;

			// Token: 0x040005FC RID: 1532
			internal ushort RequestInfoCount;

			// Token: 0x040005FD RID: 1533
			internal unsafe HttpApi.HTTP_REQUEST_INFO* pRequestInfo;
		}

		// Token: 0x0200011D RID: 285
		internal struct HTTP_SSL_PROTOCOL_INFO
		{
			// Token: 0x06000843 RID: 2115 RVA: 0x000162CC File Offset: 0x000144CC
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
				return "UNKNOWN";
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x00016368 File Offset: 0x00014568
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
				return "UNKNOWN";
			}

			// Token: 0x040005FE RID: 1534
			public uint Protocol;

			// Token: 0x040005FF RID: 1535
			public uint CipherType;

			// Token: 0x04000600 RID: 1536
			public uint CipherStrength;

			// Token: 0x04000601 RID: 1537
			public uint HashType;

			// Token: 0x04000602 RID: 1538
			public uint HashStrength;

			// Token: 0x04000603 RID: 1539
			public uint KeyExchangeType;

			// Token: 0x04000604 RID: 1540
			public uint KeyExchangeStrength;
		}
	}
}
