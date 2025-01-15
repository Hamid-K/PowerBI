using System;
using System.Collections;
using System.Collections.Generic;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000018 RID: 24
	internal class MultiRowStructure : IRfcStructure, IRfcDataContainer, ICollection, IEnumerable, IEnumerable<IRfcField>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005225 File Offset: 0x00003425
		public IEnumerable<IRfcStructure> Values
		{
			get
			{
				return this.data.Values;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005232 File Offset: 0x00003432
		public int ElementCount
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x17000048 RID: 72
		public IRfcField this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000049 RID: 73
		public IRfcField this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000524D File Offset: 0x0000344D
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005255 File Offset: 0x00003455
		public int Count { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000525E File Offset: 0x0000345E
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005266 File Offset: 0x00003466
		public object SyncRoot { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000526F File Offset: 0x0000346F
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005277 File Offset: 0x00003477
		public bool IsSynchronized { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005280 File Offset: 0x00003480
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005288 File Offset: 0x00003488
		public RfcContainerType ContainerType { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005291 File Offset: 0x00003491
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005299 File Offset: 0x00003499
		public RfcStructureMetadata Metadata { get; private set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x000052A4 File Offset: 0x000034A4
		public IRfcStructure GetStructure(int index)
		{
			IRfcStructure rfcStructure;
			if (!this.data.TryGetValue(index, out rfcStructure))
			{
				return null;
			}
			return rfcStructure;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000052C4 File Offset: 0x000034C4
		public void SetValue(int index, IRfcStructure value)
		{
			this.data[index] = value;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000052D3 File Offset: 0x000034D3
		public IEnumerator<IRfcField> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000052DA File Offset: 0x000034DA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000052E2 File Offset: 0x000034E2
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000052E9 File Offset: 0x000034E9
		public IRfcDataContainer Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000052F0 File Offset: 0x000034F0
		public RfcElementMetadata GetElementMetadata(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000052F7 File Offset: 0x000034F7
		public RfcElementMetadata GetElementMetadata(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000052FE File Offset: 0x000034FE
		public void SetValue(int index, IRfcTable value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005305 File Offset: 0x00003505
		public void SetValue(int index, IRfcAbapObject value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000530C File Offset: 0x0000350C
		public void SetValue(int index, byte value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005313 File Offset: 0x00003513
		public void SetValue(int index, byte[] value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000531A File Offset: 0x0000351A
		public void SetValue(int index, byte[] value, int offset, int len)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005321 File Offset: 0x00003521
		public void SetValue(int index, char value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005328 File Offset: 0x00003528
		public void SetValue(int index, char[] value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000532F File Offset: 0x0000352F
		public void SetValue(int index, short value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005336 File Offset: 0x00003536
		public void SetValue(int index, int value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000533D File Offset: 0x0000353D
		public void SetValue(int index, long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005344 File Offset: 0x00003544
		public void SetValue(int index, float value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000534B File Offset: 0x0000354B
		public void SetValue(int index, double value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005352 File Offset: 0x00003552
		public void SetValue(int index, decimal value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005359 File Offset: 0x00003559
		public void SetValue(int index, string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005360 File Offset: 0x00003560
		public void SetValue(int index, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005367 File Offset: 0x00003567
		public IRfcStructure GetStructure(int index, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000536E File Offset: 0x0000356E
		public IRfcTable GetTable(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005375 File Offset: 0x00003575
		public IRfcTable GetTable(int index, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000537C File Offset: 0x0000357C
		public IRfcAbapObject GetAbapObject(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005383 File Offset: 0x00003583
		public IRfcAbapObject GetAbapObject(int index, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000538A File Offset: 0x0000358A
		public byte GetByte(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005391 File Offset: 0x00003591
		public byte[] GetByteArray(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005398 File Offset: 0x00003598
		public void GetByteArray(int index, byte[] target, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000539F File Offset: 0x0000359F
		public char GetChar(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000053A6 File Offset: 0x000035A6
		public char[] GetCharArray(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000053AD File Offset: 0x000035AD
		public short GetShort(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000053B4 File Offset: 0x000035B4
		public int GetInt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000053BB File Offset: 0x000035BB
		public long GetLong(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000053C2 File Offset: 0x000035C2
		public float GetFloat(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000053C9 File Offset: 0x000035C9
		public double GetDouble(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000053D0 File Offset: 0x000035D0
		public decimal GetDecimal(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000053D7 File Offset: 0x000035D7
		public string GetString(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000053DE File Offset: 0x000035DE
		public object GetObject(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000053E5 File Offset: 0x000035E5
		public object GetValue(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000053EC File Offset: 0x000035EC
		public void SetValue(string name, IRfcStructure value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000053F3 File Offset: 0x000035F3
		public void SetValue(string name, IRfcTable value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000053FA File Offset: 0x000035FA
		public void SetValue(string name, IRfcAbapObject value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005401 File Offset: 0x00003601
		public void SetValue(string name, byte value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005408 File Offset: 0x00003608
		public void SetValue(string name, byte[] value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000540F File Offset: 0x0000360F
		public void SetValue(string name, byte[] value, int offset, int len)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005416 File Offset: 0x00003616
		public void SetValue(string name, char value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000541D File Offset: 0x0000361D
		public void SetValue(string name, char[] value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005424 File Offset: 0x00003624
		public void SetValue(string name, short value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000542B File Offset: 0x0000362B
		public void SetValue(string name, int value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005432 File Offset: 0x00003632
		public void SetValue(string name, long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005439 File Offset: 0x00003639
		public void SetValue(string name, float value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005440 File Offset: 0x00003640
		public void SetValue(string name, double value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005447 File Offset: 0x00003647
		public void SetValue(string name, decimal value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000544E File Offset: 0x0000364E
		public void SetValue(string name, string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005455 File Offset: 0x00003655
		public void SetValue(string name, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000545C File Offset: 0x0000365C
		public IRfcStructure GetStructure(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005463 File Offset: 0x00003663
		public IRfcStructure GetStructure(string name, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000546A File Offset: 0x0000366A
		public IRfcTable GetTable(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005471 File Offset: 0x00003671
		public IRfcTable GetTable(string name, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005478 File Offset: 0x00003678
		public IRfcAbapObject GetAbapObject(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000547F File Offset: 0x0000367F
		public IRfcAbapObject GetAbapObject(string name, bool create)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005486 File Offset: 0x00003686
		public byte GetByte(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000548D File Offset: 0x0000368D
		public byte[] GetByteArray(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005494 File Offset: 0x00003694
		public void GetByteArray(string name, byte[] target, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000549B File Offset: 0x0000369B
		public char GetChar(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000054A2 File Offset: 0x000036A2
		public char[] GetCharArray(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000054A9 File Offset: 0x000036A9
		public short GetShort(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000054B0 File Offset: 0x000036B0
		public int GetInt(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000054B7 File Offset: 0x000036B7
		public long GetLong(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000054BE File Offset: 0x000036BE
		public float GetFloat(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000054C5 File Offset: 0x000036C5
		public double GetDouble(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000054CC File Offset: 0x000036CC
		public decimal GetDecimal(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000054D3 File Offset: 0x000036D3
		public string GetString(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000054DA File Offset: 0x000036DA
		public object GetObject(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000054E1 File Offset: 0x000036E1
		public object GetValue(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000054E8 File Offset: 0x000036E8
		public void MoveCorrespondingTo(IRfcStructure target)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000054EF File Offset: 0x000036EF
		public bool IsStringifiable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000054F6 File Offset: 0x000036F6
		public string Stringify()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000054FD File Offset: 0x000036FD
		public void Destringify(string structureAsString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000060 RID: 96
		private readonly Dictionary<int, IRfcStructure> data = new Dictionary<int, IRfcStructure>();
	}
}
