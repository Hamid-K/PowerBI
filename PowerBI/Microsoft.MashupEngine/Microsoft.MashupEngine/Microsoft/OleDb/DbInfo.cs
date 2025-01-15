using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OleDb
{
	// Token: 0x02001E90 RID: 7824
	public class DbInfo : IDBInfo
	{
		// Token: 0x0600C165 RID: 49509 RVA: 0x0026E19C File Offset: 0x0026C39C
		public DbInfo(string keywords, IEnumerable<DbLiteralInfo> literalInfos)
		{
			this.keywords = keywords;
			this.literalInfos = new Dictionary<DBLITERAL, DbLiteralInfo>();
			foreach (DbLiteralInfo dbLiteralInfo in literalInfos)
			{
				this.literalInfos.Add(dbLiteralInfo.Literal, dbLiteralInfo);
			}
		}

		// Token: 0x0600C166 RID: 49510 RVA: 0x0026E208 File Offset: 0x0026C408
		unsafe void IDBInfo.GetKeywords(out char* _nativeKeywords)
		{
			_nativeKeywords = (IntPtr)((UIntPtr)0);
			using (ComHeap comHeap = new ComHeap())
			{
				char* ptr = comHeap.AllocString(this.keywords);
				comHeap.Commit();
				_nativeKeywords = ptr;
			}
		}

		// Token: 0x0600C167 RID: 49511 RVA: 0x0026E254 File Offset: 0x0026C454
		private unsafe DBLITERAL[] GetLiterals(uint cLiterals, DBLITERAL* nativeLiterals)
		{
			if (cLiterals == 0U)
			{
				return this.literalInfos.Keys.ToArray<DBLITERAL>();
			}
			DBLITERAL[] array = new DBLITERAL[cLiterals];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = nativeLiterals[i];
			}
			return array;
		}

		// Token: 0x0600C168 RID: 49512 RVA: 0x0026E298 File Offset: 0x0026C498
		unsafe int IDBInfo.GetLiteralInfo(uint cLiterals, DBLITERAL* nativeLiterals, out uint cLiteralInfo, out DBLITERALINFO* _nativeLiteralInfos, out char* _nativeStrings)
		{
			cLiteralInfo = 0U;
			_nativeLiteralInfos = (IntPtr)((UIntPtr)0);
			_nativeStrings = (IntPtr)((UIntPtr)0);
			int num3;
			using (ComHeap comHeap = new ComHeap())
			{
				DBLITERAL[] literals = this.GetLiterals(cLiterals, nativeLiterals);
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
						ptr3->literalValue = ptr + num2;
						num2 += dbLiteralInfo2.LiteralValue.Length + 1;
						ptr3->invalidChars = ptr + num2;
						num2 += dbLiteralInfo2.InvalidChars.Length + 1;
						ptr3->invalidStartingChars = ptr + num2;
						num2 += dbLiteralInfo2.InvalidStartingChars.Length + 1;
						ptr3->literal = dbliteral2;
						ptr3->supported = 1;
						ptr3->maxLength = (uint)dbLiteralInfo2.MaxLength;
						num++;
					}
					else
					{
						ptr3->literalValue = null;
						ptr3->invalidChars = null;
						ptr3->invalidStartingChars = null;
						ptr3->literal = dbliteral2;
						ptr3->supported = 0;
						ptr3->maxLength = 0U;
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
					cLiteralInfo = (uint)literals.Length;
					_nativeLiteralInfos = ptr2;
					_nativeStrings = ptr;
					num3 = ((num == literals.Length) ? 0 : 265946);
				}
			}
			return num3;
		}

		// Token: 0x04006194 RID: 24980
		private readonly string keywords;

		// Token: 0x04006195 RID: 24981
		private readonly Dictionary<DBLITERAL, DbLiteralInfo> literalInfos;
	}
}
