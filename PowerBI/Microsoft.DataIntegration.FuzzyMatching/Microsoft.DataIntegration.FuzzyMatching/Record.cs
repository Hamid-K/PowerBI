using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D2 RID: 210
	[SqlUserDefinedType(2, MaxByteSize = -1, Name = "Record")]
	[Serializable]
	public class Record : DataRecordImplBase, INullable, IXmlSerializable, IBinarySerialize
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002679D File Offset: 0x0002499D
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x000267A5 File Offset: 0x000249A5
		public object[] Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		// Token: 0x1700017B RID: 379
		public override object this[int i]
		{
			get
			{
				if (!this.IsNull)
				{
					return this.Values[i];
				}
				return null;
			}
			set
			{
				this.Values[i] = value;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000267CD File Offset: 0x000249CD
		public override DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000267D4 File Offset: 0x000249D4
		public override int FieldCount
		{
			get
			{
				return this.Values.Length;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000267DE File Offset: 0x000249DE
		public Record()
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000267E6 File Offset: 0x000249E6
		public void Clear()
		{
			if (this.Values != null)
			{
				Array.Clear(this.Values, 0, this.Values.Length);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026804 File Offset: 0x00024A04
		public Record(BinaryReader r)
		{
			this.Read(r);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026813 File Offset: 0x00024A13
		public Record(BinaryReader r, ISegmentAllocator<char> allocator)
		{
			this.Read(r, allocator);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026823 File Offset: 0x00024A23
		private static void Write(BinaryWriter writer, SqlChars chars)
		{
			if (chars.IsNull)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(chars.Length);
			writer.Write(chars.Buffer, 0, (int)chars.Length);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026858 File Offset: 0x00024A58
		public static SqlBytes Concat(SqlBytes record1, SqlBytes record2)
		{
			Record record3 = new Record(new BinaryReader(record1.Stream));
			Record record4 = new Record(new BinaryReader(record2.Stream));
			Record record5 = new Record
			{
				m_values = new object[record3.FieldCount + record4.FieldCount]
			};
			record3.m_values.CopyTo(record5.m_values, 0);
			record4.m_values.CopyTo(record5.m_values, record3.FieldCount);
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			record5.Write(binaryWriter);
			binaryWriter.Flush();
			memoryStream.Flush();
			memoryStream.SetLength(memoryStream.Position);
			memoryStream.Seek(0L, 0);
			return new SqlBytes(memoryStream);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026908 File Offset: 0x00024B08
		public static SqlBytes CreateBinary(SqlBytes bytes)
		{
			Record record = new Record
			{
				m_values = new object[] { bytes.Value }
			};
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			record.Write(binaryWriter);
			binaryWriter.Flush();
			memoryStream.Flush();
			memoryStream.SetLength(memoryStream.Position);
			memoryStream.Seek(0L, 0);
			return new SqlBytes(memoryStream);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002696C File Offset: 0x00024B6C
		public static SqlBytes CreateChars(SqlChars chars)
		{
			Record record = new Record
			{
				m_values = new object[] { chars.Value }
			};
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			record.Write(binaryWriter);
			binaryWriter.Flush();
			memoryStream.Flush();
			memoryStream.SetLength(memoryStream.Position);
			memoryStream.Seek(0L, 0);
			return new SqlBytes(memoryStream);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000269D0 File Offset: 0x00024BD0
		public static SqlBytes CreateSqlBytes1(object s)
		{
			return Record.CreateSqlBytes(new object[] { s });
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000269E1 File Offset: 0x00024BE1
		public static SqlBytes CreateSqlBytes2(object s1, object s2)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2 });
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000269F6 File Offset: 0x00024BF6
		public static SqlBytes CreateSqlBytes3(object s1, object s2, object s3)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3 });
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00026A0F File Offset: 0x00024C0F
		public static SqlBytes CreateSqlBytes4(object s1, object s2, object s3, object s4)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4 });
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00026A2C File Offset: 0x00024C2C
		public static SqlBytes CreateSqlBytes5(object s1, object s2, object s3, object s4, object s5)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00026A4E File Offset: 0x00024C4E
		public static SqlBytes CreateSqlBytes6(object s1, object s2, object s3, object s4, object s5, object s6)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00026A75 File Offset: 0x00024C75
		public static SqlBytes CreateSqlBytes7(object s1, object s2, object s3, object s4, object s5, object s6, object s7)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00026AA1 File Offset: 0x00024CA1
		public static SqlBytes CreateSqlBytes8(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00026AD2 File Offset: 0x00024CD2
		public static SqlBytes CreateSqlBytes9(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00026B09 File Offset: 0x00024D09
		public static SqlBytes CreateSqlBytes10(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10)
		{
			return Record.CreateSqlBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00026B48 File Offset: 0x00024D48
		public static SqlBytes CreateSqlBytes11(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00026B98 File Offset: 0x00024D98
		public static SqlBytes CreateSqlBytes12(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00026BEC File Offset: 0x00024DEC
		public static SqlBytes CreateSqlBytes13(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00026C48 File Offset: 0x00024E48
		public static SqlBytes CreateSqlBytes14(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00026CA8 File Offset: 0x00024EA8
		public static SqlBytes CreateSqlBytes15(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00026D10 File Offset: 0x00024F10
		public static SqlBytes CreateSqlBytes16(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15, object s16)
		{
			return Record.CreateSqlBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00026D7C File Offset: 0x00024F7C
		public static SqlBytes CreateSqlBytes(params object[] values)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			MemoryStream memoryStream = binaryWriter.BaseStream as MemoryStream;
			Record.Write(binaryWriter, values);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			binaryWriter.BaseStream.SetLength(memoryStream.Position);
			binaryWriter.BaseStream.Seek(0L, 0);
			binaryWriter.Seek(0, 0);
			return new SqlBytes(memoryStream.ToArray());
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00026DEA File Offset: 0x00024FEA
		public static SqlBinary CreateSqlBinaryFromString1(string s)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s });
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00026DFB File Offset: 0x00024FFB
		public static SqlBinary CreateSqlBinaryFromString2(string s1, string s2)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2 });
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00026E10 File Offset: 0x00025010
		public static SqlBinary CreateSqlBinaryFromString3(string s1, string s2, string s3)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3 });
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00026E29 File Offset: 0x00025029
		public static SqlBinary CreateSqlBinaryFromString4(string s1, string s2, string s3, string s4)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4 });
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00026E46 File Offset: 0x00025046
		public static SqlBinary CreateSqlBinaryFromString5(string s1, string s2, string s3, string s4, string s5)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00026E68 File Offset: 0x00025068
		public static SqlBinary CreateSqlBinaryFromString6(string s1, string s2, string s3, string s4, string s5, string s6)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00026E8F File Offset: 0x0002508F
		public static SqlBinary CreateSqlBinaryFromString7(string s1, string s2, string s3, string s4, string s5, string s6, string s7)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00026EBB File Offset: 0x000250BB
		public static SqlBinary CreateSqlBinaryFromString8(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00026EEC File Offset: 0x000250EC
		public static SqlBinary CreateSqlBinaryFromString9(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00026F23 File Offset: 0x00025123
		public static SqlBinary CreateSqlBinaryFromString10(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10)
		{
			return Record.CreateSqlBinaryFromString(new string[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00026F60 File Offset: 0x00025160
		public static SqlBinary CreateSqlBinaryFromString11(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00026FB0 File Offset: 0x000251B0
		public static SqlBinary CreateSqlBinaryFromString12(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00027004 File Offset: 0x00025204
		public static SqlBinary CreateSqlBinaryFromString13(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00027060 File Offset: 0x00025260
		public static SqlBinary CreateSqlBinaryFromString14(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000270C0 File Offset: 0x000252C0
		public static SqlBinary CreateSqlBinaryFromString15(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00027128 File Offset: 0x00025328
		public static SqlBinary CreateSqlBinaryFromString16(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15, string s16)
		{
			return Record.CreateSqlBinaryFromString(new string[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00027194 File Offset: 0x00025394
		public static SqlBinary CreateSqlBinaryFromString(params string[] values)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			MemoryStream memoryStream = binaryWriter.BaseStream as MemoryStream;
			Record.Write(binaryWriter, values);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			binaryWriter.BaseStream.SetLength(memoryStream.Position);
			binaryWriter.BaseStream.Seek(0L, 0);
			binaryWriter.Seek(0, 0);
			return new SqlBinary(memoryStream.ToArray());
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00027204 File Offset: 0x00025404
		public static SqlBinary CreateSqlBinary1(object s)
		{
			return Record.CreateSqlBinary(new object[] { s });
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00027215 File Offset: 0x00025415
		public static SqlBinary CreateSqlBinary2(object s1, object s2)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2 });
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002722A File Offset: 0x0002542A
		public static SqlBinary CreateSqlBinary3(object s1, object s2, object s3)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3 });
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00027243 File Offset: 0x00025443
		public static SqlBinary CreateSqlBinary4(object s1, object s2, object s3, object s4)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4 });
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00027260 File Offset: 0x00025460
		public static SqlBinary CreateSqlBinary5(object s1, object s2, object s3, object s4, object s5)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00027282 File Offset: 0x00025482
		public static SqlBinary CreateSqlBinary6(object s1, object s2, object s3, object s4, object s5, object s6)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000272A9 File Offset: 0x000254A9
		public static SqlBinary CreateSqlBinary7(object s1, object s2, object s3, object s4, object s5, object s6, object s7)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000272D5 File Offset: 0x000254D5
		public static SqlBinary CreateSqlBinary8(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00027306 File Offset: 0x00025506
		public static SqlBinary CreateSqlBinary9(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002733D File Offset: 0x0002553D
		public static SqlBinary CreateSqlBinary10(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10)
		{
			return Record.CreateSqlBinary(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002737C File Offset: 0x0002557C
		public static SqlBinary CreateSqlBinary11(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000273CC File Offset: 0x000255CC
		public static SqlBinary CreateSqlBinary12(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00027420 File Offset: 0x00025620
		public static SqlBinary CreateSqlBinary13(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002747C File Offset: 0x0002567C
		public static SqlBinary CreateSqlBinary14(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000274DC File Offset: 0x000256DC
		public static SqlBinary CreateSqlBinary15(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00027544 File Offset: 0x00025744
		public static SqlBinary CreateSqlBinary16(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15, object s16)
		{
			return Record.CreateSqlBinary(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000275B0 File Offset: 0x000257B0
		public static SqlBinary CreateSqlBinary(params object[] values)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			MemoryStream memoryStream = binaryWriter.BaseStream as MemoryStream;
			Record.Write(binaryWriter, values);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			binaryWriter.BaseStream.SetLength(memoryStream.Position);
			binaryWriter.BaseStream.Seek(0L, 0);
			binaryWriter.Seek(0, 0);
			return new SqlBinary(memoryStream.ToArray());
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002761E File Offset: 0x0002581E
		public static Record Create(object s)
		{
			return Record.Create(new object[] { s });
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002762F File Offset: 0x0002582F
		public static Record Create(object s1, object s2)
		{
			return Record.Create(new object[] { s1, s2 });
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00027644 File Offset: 0x00025844
		public static Record Create(object s1, object s2, object s3)
		{
			return Record.Create(new object[] { s1, s2, s3 });
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002765D File Offset: 0x0002585D
		public static Record Create(object s1, object s2, object s3, object s4)
		{
			return Record.Create(new object[] { s1, s2, s3, s4 });
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002767A File Offset: 0x0002587A
		public static Record Create(object s1, object s2, object s3, object s4, object s5)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002769C File Offset: 0x0002589C
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000276C3 File Offset: 0x000258C3
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000276EF File Offset: 0x000258EF
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00027720 File Offset: 0x00025920
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00027757 File Offset: 0x00025957
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10)
		{
			return Record.Create(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027794 File Offset: 0x00025994
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000277E4 File Offset: 0x000259E4
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00027838 File Offset: 0x00025A38
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00027894 File Offset: 0x00025A94
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000278F4 File Offset: 0x00025AF4
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002795C File Offset: 0x00025B5C
		public static Record Create(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15, object s16)
		{
			return Record.Create(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000279C8 File Offset: 0x00025BC8
		public static Record Create(params object[] values)
		{
			return new Record
			{
				Values = values
			};
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000279D6 File Offset: 0x00025BD6
		public static byte[] CreateS1(string s)
		{
			return Record.CreateBytes(new object[] { s });
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000279E7 File Offset: 0x00025BE7
		public static byte[] CreateS2(string s1, string s2)
		{
			return Record.CreateBytes(new object[] { s1, s2 });
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000279FC File Offset: 0x00025BFC
		public static byte[] CreateS3(string s1, string s2, string s3)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3 });
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00027A15 File Offset: 0x00025C15
		public static byte[] CreateS4(string s1, string s2, string s3, string s4)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4 });
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00027A32 File Offset: 0x00025C32
		public static byte[] CreateS5(string s1, string s2, string s3, string s4, string s5)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027A54 File Offset: 0x00025C54
		public static byte[] CreateS6(string s1, string s2, string s3, string s4, string s5, string s6)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00027A7B File Offset: 0x00025C7B
		public static byte[] CreateS7(string s1, string s2, string s3, string s4, string s5, string s6, string s7)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00027AA7 File Offset: 0x00025CA7
		public static byte[] CreateS8(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00027AD8 File Offset: 0x00025CD8
		public static byte[] CreateS9(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00027B0F File Offset: 0x00025D0F
		public static byte[] CreateS10(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00027B4C File Offset: 0x00025D4C
		public static byte[] CreateS11(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00027B9C File Offset: 0x00025D9C
		public static byte[] CreateS12(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00027BF0 File Offset: 0x00025DF0
		public static byte[] CreateS13(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00027C4C File Offset: 0x00025E4C
		public static byte[] CreateS14(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00027CAC File Offset: 0x00025EAC
		public static byte[] CreateS15(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00027D14 File Offset: 0x00025F14
		public static byte[] CreateS16(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15, string s16)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00027D80 File Offset: 0x00025F80
		public static byte[] Create1(object s)
		{
			return Record.CreateBytes(new object[] { s });
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00027D91 File Offset: 0x00025F91
		public static byte[] Create2(object s1, object s2)
		{
			return Record.CreateBytes(new object[] { s1, s2 });
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00027DA6 File Offset: 0x00025FA6
		public static byte[] Create3(object s1, object s2, object s3)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3 });
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00027DBF File Offset: 0x00025FBF
		public static byte[] Create4(object s1, object s2, object s3, object s4)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4 });
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00027DDC File Offset: 0x00025FDC
		public static byte[] Create5(object s1, object s2, object s3, object s4, object s5)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5 });
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00027DFE File Offset: 0x00025FFE
		public static byte[] Create6(object s1, object s2, object s3, object s4, object s5, object s6)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6 });
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00027E25 File Offset: 0x00026025
		public static byte[] Create7(object s1, object s2, object s3, object s4, object s5, object s6, object s7)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7 });
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00027E51 File Offset: 0x00026051
		public static byte[] Create8(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8 });
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00027E82 File Offset: 0x00026082
		public static byte[] Create9(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9 });
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00027EB9 File Offset: 0x000260B9
		public static byte[] Create10(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10)
		{
			return Record.CreateBytes(new object[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00027EF8 File Offset: 0x000260F8
		public static byte[] Create11(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11
			});
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00027F48 File Offset: 0x00026148
		public static byte[] Create12(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12
			});
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00027F9C File Offset: 0x0002619C
		public static byte[] Create13(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13
			});
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00027FF8 File Offset: 0x000261F8
		public static byte[] Create14(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14
			});
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00028058 File Offset: 0x00026258
		public static byte[] Create15(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15
			});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000280C0 File Offset: 0x000262C0
		public static byte[] Create16(object s1, object s2, object s3, object s4, object s5, object s6, object s7, object s8, object s9, object s10, object s11, object s12, object s13, object s14, object s15, object s16)
		{
			return Record.CreateBytes(new object[]
			{
				s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,
				s11, s12, s13, s14, s15, s16
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002812C File Offset: 0x0002632C
		public static byte[] CreateBytes(params object[] values)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			MemoryStream memoryStream = binaryWriter.BaseStream as MemoryStream;
			Record.Write(binaryWriter, values);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			binaryWriter.BaseStream.SetLength(memoryStream.Position);
			binaryWriter.BaseStream.Seek(0L, 0);
			binaryWriter.Seek(0, 0);
			return memoryStream.ToArray();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00028198 File Offset: 0x00026398
		public void Read(BinaryReader r)
		{
			ISegmentAllocator<char> instance = HeapSegmentAllocator<char>.Instance;
			this.Read(r, instance);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000281B4 File Offset: 0x000263B4
		public void Read(BinaryReader r, ISegmentAllocator<char> allocator)
		{
			int num = r.ReadInt32();
			if (this.m_values == null || this.m_values.Length != num)
			{
				this.m_values = new object[num];
			}
			int i = 0;
			while (i < num)
			{
				switch (r.ReadInt16())
				{
				case -1:
					this.m_values[i] = null;
					break;
				case 0:
				case 4:
				case 5:
				case 13:
				case 14:
				case 16:
				case 17:
				case 21:
				case 22:
					goto IL_0266;
				case 1:
				{
					int num2 = r.ReadInt32();
					if (num2 > 0)
					{
						this.m_values[i] = r.ReadBytes(num2);
					}
					else
					{
						this.m_values[i] = new byte[0];
					}
					break;
				}
				case 2:
					this.m_values[i] = r.ReadByte();
					break;
				case 3:
					this.m_values[i] = r.ReadBoolean();
					break;
				case 6:
					this.m_values[i] = new DateTime(r.ReadInt64());
					break;
				case 7:
					this.m_values[i] = r.ReadDecimal();
					break;
				case 8:
					this.m_values[i] = r.ReadDouble();
					break;
				case 9:
					this.m_values[i] = new Guid(r.ReadBytes(16));
					break;
				case 10:
					this.m_values[i] = r.ReadInt16();
					break;
				case 11:
					this.m_values[i] = r.ReadInt32();
					break;
				case 12:
					this.m_values[i] = r.ReadInt64();
					break;
				case 15:
					this.m_values[i] = r.ReadSingle();
					break;
				case 18:
					this.m_values[i] = r.ReadUInt16();
					break;
				case 19:
					this.m_values[i] = r.ReadUInt32();
					break;
				case 20:
					this.m_values[i] = r.ReadUInt64();
					break;
				case 23:
				{
					ArraySegment<char> arraySegment = allocator.New(r.ReadInt32());
					if (arraySegment.Count > 0)
					{
						r.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					}
					this.m_values[i] = arraySegment;
					break;
				}
				default:
					goto IL_0266;
				}
				i++;
				continue;
				IL_0266:
				throw new Exception("Unexpected field type encountered.");
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002843D File Offset: 0x0002663D
		public void Write(BinaryWriter w)
		{
			Record.Write(w, this.Values);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002844C File Offset: 0x0002664C
		private static void Write(BinaryWriter w, object[] values)
		{
			if (values != null)
			{
				w.Write(values.Length);
				foreach (object obj in values)
				{
					Record.WriteValue(w, obj);
				}
				return;
			}
			w.Write(0);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00028488 File Offset: 0x00026688
		private static void WriteValue(BinaryWriter w, object s)
		{
			if (s == null || s is DBNull)
			{
				w.Write(-1);
				return;
			}
			if (s is char[])
			{
				char[] array = s as char[];
				w.Write(23);
				w.Write(array.Length);
				w.Write(array, 0, array.Length);
				return;
			}
			if (s is string)
			{
				char[] array2 = ((string)s).ToCharArray();
				w.Write(23);
				w.Write(array2.Length);
				w.Write(array2, 0, array2.Length);
				return;
			}
			if (s is ArraySegment<char>)
			{
				ArraySegment<char> arraySegment = (ArraySegment<char>)s;
				w.Write(23);
				w.Write(arraySegment.Count);
				w.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				return;
			}
			if (s is SqlString)
			{
				char[] array3 = ((SqlString)s).Value.ToCharArray();
				w.Write(23);
				w.Write(array3.Length);
				w.Write(array3, 0, array3.Length);
				return;
			}
			if (s is byte[])
			{
				byte[] array4 = (byte[])s;
				w.Write(1);
				w.Write(array4.Length);
				w.Write(array4);
				return;
			}
			if (s is SqlBytes)
			{
				byte[] value = ((SqlBytes)s).Value;
				w.Write(1);
				w.Write(value.Length);
				w.Write(value);
				return;
			}
			if (s is SqlBinary)
			{
				byte[] value2 = ((SqlBinary)s).Value;
				w.Write(1);
				w.Write(value2.Length);
				w.Write(value2);
				return;
			}
			if (s is byte)
			{
				w.Write(2);
				w.Write((byte)s);
				return;
			}
			if (s is bool)
			{
				w.Write(3);
				w.Write((bool)s);
				return;
			}
			if (s is DateTime)
			{
				w.Write(6);
				w.Write(((DateTime)s).Ticks);
				return;
			}
			if (s is decimal)
			{
				w.Write(7);
				w.Write((decimal)s);
				return;
			}
			if (s is double)
			{
				w.Write(8);
				w.Write((double)s);
				return;
			}
			if (s is Guid)
			{
				w.Write(9);
				w.Write(((Guid)s).ToByteArray());
				return;
			}
			if (s is short)
			{
				w.Write(10);
				w.Write((short)s);
				return;
			}
			if (s is int)
			{
				w.Write(11);
				w.Write((int)s);
				return;
			}
			if (s is long)
			{
				w.Write(12);
				w.Write((long)s);
				return;
			}
			if (s is float)
			{
				w.Write(15);
				w.Write((float)s);
				return;
			}
			if (s is ushort)
			{
				w.Write(18);
				w.Write((ushort)s);
				return;
			}
			if (s is uint)
			{
				w.Write(19);
				w.Write((uint)s);
				return;
			}
			if (s is ulong)
			{
				w.Write(20);
				w.Write((ulong)s);
				return;
			}
			if (s is SqlByte)
			{
				w.Write(2);
				w.Write(((SqlByte)s).Value);
				return;
			}
			if (s is SqlBoolean)
			{
				w.Write(3);
				w.Write(((SqlBoolean)s).Value);
				return;
			}
			if (s is SqlDateTime)
			{
				w.Write(6);
				w.Write(((SqlDateTime)s).Value.Ticks);
				return;
			}
			if (s is SqlDecimal)
			{
				w.Write(7);
				w.Write(((SqlDecimal)s).Value);
				return;
			}
			if (s is SqlDouble)
			{
				w.Write(8);
				w.Write(((SqlDouble)s).Value);
				return;
			}
			if (s is SqlGuid)
			{
				w.Write(9);
				w.Write(((SqlGuid)s).ToByteArray());
				return;
			}
			if (s is SqlInt16)
			{
				w.Write(10);
				w.Write(((SqlInt16)s).Value);
				return;
			}
			if (s is SqlInt32)
			{
				w.Write(11);
				w.Write(((SqlInt32)s).Value);
				return;
			}
			if (s is SqlInt64)
			{
				w.Write(12);
				w.Write(((SqlInt64)s).Value);
				return;
			}
			if (s is SqlSingle)
			{
				w.Write(15);
				w.Write(((SqlSingle)s).Value);
				return;
			}
			throw new InvalidOperationException(string.Format("Unsupported type: {0}", s.GetType().ToString()));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002892B File Offset: 0x00026B2B
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00028930 File Offset: 0x00026B30
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			List<string> list = new List<string>();
			if (reader.ReadToFollowing("Record"))
			{
				while (reader.ReadToFollowing("Column"))
				{
					list.Add(reader.ReadElementString());
				}
			}
			object[] array = list.ToArray();
			this.Values = array;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002897C File Offset: 0x00026B7C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Record");
			if (this.Values != null)
			{
				foreach (string text in this.Values)
				{
					writer.WriteElementString("Column", text);
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000289CC File Offset: 0x00026BCC
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter();
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
			this.WriteXml(xmlWriter);
			xmlWriter.Flush();
			return stringWriter.ToString();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000289F7 File Offset: 0x00026BF7
		public static Record Parse(SqlString s)
		{
			if (s.IsNull)
			{
				return Record.Null;
			}
			Record record = new Record();
			record.ReadXml(XmlReader.Create(new StringReader(s.Value)));
			return record;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00028A24 File Offset: 0x00026C24
		public bool IsNull
		{
			get
			{
				return this.Values == null;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00028A2F File Offset: 0x00026C2F
		public static Record Null
		{
			get
			{
				return new Record();
			}
		}

		// Token: 0x0400034F RID: 847
		private const int DbTypeNull = -1;

		// Token: 0x04000350 RID: 848
		private object[] m_values;
	}
}
