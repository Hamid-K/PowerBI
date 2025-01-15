using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003B RID: 59
	public class DbInfo : IDBInfo
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00006254 File Offset: 0x00004454
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public DbInfo(string keywords, IEnumerable<DbLiteralInfo> literalInfos)
		{
			this.keywords = keywords;
			this.literalInfos = new Dictionary<DBLITERAL, DbLiteralInfo>();
			foreach (DbLiteralInfo dbLiteralInfo in literalInfos)
			{
				this.literalInfos.Add(dbLiteralInfo.Literal, dbLiteralInfo);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000062C0 File Offset: 0x000044C0
		unsafe void IDBInfo.GetKeywords(out char* nativeKeywords)
		{
			nativeKeywords = (IntPtr)((UIntPtr)0);
			using (ComHeap comHeap = new ComHeap())
			{
				char* ptr = comHeap.AllocString(this.keywords);
				comHeap.Commit();
				nativeKeywords = ptr;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000630C File Offset: 0x0000450C
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe DBLITERAL[] GetLiterals(uint literalCount, DBLITERAL* nativeLiterals)
		{
			if (literalCount == 0U)
			{
				return this.literalInfos.Keys.ToArray<DBLITERAL>();
			}
			DBLITERAL[] array = new DBLITERAL[literalCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = nativeLiterals[i];
			}
			return array;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006350 File Offset: 0x00004550
		unsafe int IDBInfo.GetLiteralInfo(uint literalCount, DBLITERAL* nativeLiterals, out uint literalInfoCount, out DBLITERALINFO* nativeLiteralInfos, out char* nativeStrings)
		{
			literalInfoCount = 0U;
			nativeLiteralInfos = (IntPtr)((UIntPtr)0);
			nativeStrings = (IntPtr)((UIntPtr)0);
			int num3;
			using (ComHeap comHeap = new ComHeap())
			{
				DBLITERAL[] literals = this.GetLiterals(literalCount, nativeLiterals);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DBLITERAL dbliteral in literals)
				{
					DbLiteralInfo dbLiteralInfo;
					if (this.literalInfos.TryGetValue(dbliteral, out dbLiteralInfo))
					{
						stringBuilder.Append(dbLiteralInfo.LiteralValue);
						stringBuilder.Append('\0');
						stringBuilder.Append(dbLiteralInfo.InvalidChars);
						stringBuilder.Append('\0');
						stringBuilder.Append(dbLiteralInfo.InvalidStartingChars);
						stringBuilder.Append('\0');
					}
				}
				char* ptr = comHeap.AllocString(stringBuilder.ToString());
				int num = 0;
				int num2 = 0;
				DBLITERALINFO* ptr2 = (DBLITERALINFO*)comHeap.AllocArray(literals.Length, sizeof(DBLITERALINFO));
				for (int j = 0; j < literals.Length; j++)
				{
					DBLITERAL dbliteral2 = nativeLiterals[j];
					DBLITERALINFO* ptr3 = ptr2 + j;
					DbLiteralInfo dbLiteralInfo2;
					if (this.literalInfos.TryGetValue(dbliteral2, out dbLiteralInfo2))
					{
						ptr3->LiteralValue = ptr + num2;
						num2 += dbLiteralInfo2.LiteralValue.Length + 1;
						ptr3->InvalidChars = ptr + num2;
						num2 += dbLiteralInfo2.InvalidChars.Length + 1;
						ptr3->InvalidStartingChars = ptr + num2;
						num2 += dbLiteralInfo2.InvalidStartingChars.Length + 1;
						ptr3->Literal = dbliteral2;
						ptr3->Supported = 1;
						ptr3->MaxLength = (uint)dbLiteralInfo2.MaxLength;
						num++;
					}
					else
					{
						ptr3->LiteralValue = null;
						ptr3->InvalidChars = null;
						ptr3->InvalidStartingChars = null;
						ptr3->Literal = dbliteral2;
						ptr3->Supported = 0;
						ptr3->MaxLength = 0U;
					}
				}
				if (literals.Length == 0)
				{
					num3 = 0;
				}
				else if (num == 0)
				{
					num3 = -2147217887;
				}
				else
				{
					comHeap.Commit();
					literalInfoCount = (uint)literals.Length;
					nativeLiteralInfos = ptr2;
					nativeStrings = ptr;
					num3 = ((num == literals.Length) ? 0 : 265946);
				}
			}
			return num3;
		}

		// Token: 0x0400007F RID: 127
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly string keywords;

		// Token: 0x04000080 RID: 128
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly Dictionary<DBLITERAL, DbLiteralInfo> literalInfos;
	}
}
