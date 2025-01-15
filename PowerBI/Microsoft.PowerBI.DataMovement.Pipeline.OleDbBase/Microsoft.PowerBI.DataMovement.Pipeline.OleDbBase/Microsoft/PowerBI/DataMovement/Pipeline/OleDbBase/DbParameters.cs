using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003F RID: 63
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class DbParameters
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000676C File Offset: 0x0000496C
		public DbParameters()
		{
			this.parameters = new SortedDictionary<uint, DbParameter>();
			this.parameter2Ordinal = new Dictionary<string, uint>();
			this.typeTranslator = new Dictionary<string, DBTYPE>
			{
				{
					"DBTYPE_I1",
					DBTYPE.I1
				},
				{
					"DBTYPE_I2",
					DBTYPE.I2
				},
				{
					"DBTYPE_I4",
					DBTYPE.I4
				},
				{
					"DBTYPE_I8",
					DBTYPE.I8
				},
				{
					"DBTYPE_UI1",
					DBTYPE.UI1
				},
				{
					"DBTYPE_UI2",
					DBTYPE.UI2
				},
				{
					"DBTYPE_UI4",
					DBTYPE.UI4
				},
				{
					"DBTYPE_UI8",
					DBTYPE.UI8
				},
				{
					"DBTYPE_R4",
					DBTYPE.R4
				},
				{
					"DBTYPE_R8",
					DBTYPE.R8
				},
				{
					"DBTYPE_CY",
					DBTYPE.CY
				},
				{
					"DBTYPE_DECIMAL",
					DBTYPE.DECIMAL
				},
				{
					"DBTYPE_NUMERIC",
					DBTYPE.NUMERIC
				},
				{
					"DBTYPE_BOOL",
					DBTYPE.BOOL
				},
				{
					"DBTYPE_ERROR",
					DBTYPE.ERROR
				},
				{
					"DBTYPE_UDT",
					DBTYPE.UDT
				},
				{
					"DBTYPE_VARIANT",
					DBTYPE.VARIANT
				},
				{
					"DBTYPE_IDISPATCH",
					DBTYPE.IDISPATCH
				},
				{
					"DBTYPE_IUNKNOWN",
					DBTYPE.IUNKNOWN
				},
				{
					"DBTYPE_GUID",
					DBTYPE.GUID
				},
				{
					"DBTYPE_DATE",
					DBTYPE.DATE
				},
				{
					"DBTYPE_DBDATE",
					DBTYPE.DBDATE
				},
				{
					"DBTYPE_DBTIME",
					DBTYPE.DBTIME
				},
				{
					"DBTYPE_DBTIMESTAMP",
					DBTYPE.DBTIMESTAMP
				},
				{
					"DBTYPE_BSTR",
					DBTYPE.BSTR
				},
				{
					"DBTYPE_CHAR",
					DBTYPE.STR
				},
				{
					"DBTYPE_VARCHAR",
					DBTYPE.STR
				},
				{
					"DBTYPE_LONGVARCHAR",
					DBTYPE.STR
				},
				{
					"DBTYPE_WCHAR",
					DBTYPE.WSTR
				},
				{
					"DBTYPE_WVARCHAR",
					DBTYPE.WSTR
				},
				{
					"DBTYPE_WLONGVARCHAR",
					DBTYPE.WSTR
				},
				{
					"DBTYPE_BINARY",
					DBTYPE.BYTES
				},
				{
					"DBTYPE_VARBINARY",
					DBTYPE.BYTES
				},
				{
					"DBTYPE_LONGVARBINARY",
					DBTYPE.BYTES
				},
				{
					"DBTYPE_FILETIME",
					DBTYPE.FILETIME
				},
				{
					"DBTYPE_VARNUMERIC",
					DBTYPE.VARNUMERIC
				},
				{
					"DBTYPE_PROPVARIANT",
					DBTYPE.PROPVARIANT
				}
			};
			this.typeReverseTranslator = new Dictionary<DBTYPE, string>();
			foreach (string text in this.typeTranslator.Keys)
			{
				this.typeReverseTranslator[this.typeTranslator[text]] = text;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006A14 File Offset: 0x00004C14
		public string GetTypeName(DBTYPE dbType)
		{
			return this.typeReverseTranslator[dbType];
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006A24 File Offset: 0x00004C24
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe int GetParameterInfo(out DB_UPARAMS paramCount, out DBPARAMINFO* paramInfo, char** namesBuffer)
		{
			uint count = (uint)this.parameters.Count;
			paramCount.Value = (ulong)count;
			using (ComHeap comHeap = new ComHeap())
			{
				paramInfo = comHeap.AllocArray((int)count, sizeof(DBPARAMINFO));
				int num = 0;
				foreach (DbParameter dbParameter in this.parameters.Values)
				{
					num += ((dbParameter.Name == null) ? 0 : (dbParameter.Name.Length + 1));
				}
				*(IntPtr*)namesBuffer = comHeap.AllocArray(num, 2);
				byte* ptr = *(IntPtr*)namesBuffer;
				int num2 = 0;
				foreach (uint num3 in this.parameters.Keys)
				{
					DbParameter dbParameter2 = this.parameters[num3];
					DBPARAMINFO* ptr2 = paramInfo + (IntPtr)(num2++);
					ptr2->Flags = dbParameter2.Flags;
					ptr2->Ordinal.Value = (ulong)dbParameter2.Ordinal;
					int num4 = (dbParameter2.Name.Length + 1) * 2;
					try
					{
						fixed (string text = dbParameter2.Name)
						{
							char* ptr3 = text;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							Buffer.MemoryCopy((void*)ptr3, (void*)ptr, (long)num4, (long)num4);
						}
					}
					finally
					{
						string text = null;
					}
					ptr2->Name = (char*)ptr;
					ptr += num4;
					ptr2->TypeInfo = null;
					ptr2->ParamSize = dbParameter2.ParamSize;
					ptr2->Type = (this.typeTranslator.ContainsKey(dbParameter2.TypeName) ? this.typeTranslator[dbParameter2.TypeName] : DBTYPE.ERROR);
					ptr2->Precision = dbParameter2.Precision;
					ptr2->Scale = dbParameter2.Scale;
				}
				comHeap.Commit();
			}
			return 0;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006C6C File Offset: 0x00004E6C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe int SetParameterInfo(DB_UPARAMS paramCount, DB_UPARAMS* paramOrdinals, DBPARAMBINDINFO* paramBindInfos)
		{
			int num = (int)paramCount.Value;
			if (num == 0)
			{
				this.parameters.Clear();
				this.parameter2Ordinal.Clear();
			}
			else
			{
				Dictionary<uint, DbParameter> dictionary = new Dictionary<uint, DbParameter>();
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)num))
				{
					DBPARAMBINDINFO* ptr = paramBindInfos + (ulong)num2 * (ulong)((long)sizeof(DBPARAMBINDINFO)) / (ulong)sizeof(DBPARAMBINDINFO);
					uint num3 = (uint)paramOrdinals[(ulong)num2 * (ulong)((long)sizeof(DB_UPARAMS)) / (ulong)sizeof(DB_UPARAMS)].Value;
					if (num3 == 0U)
					{
						return -2147024809;
					}
					dictionary[num3] = new DbParameter
					{
						Ordinal = num3,
						Flags = ptr->Flags,
						Name = ((null != ptr->Name) ? new string(ptr->Name) : null),
						ParamSize = ptr->ParamSize,
						TypeName = new string(ptr->DataSourceType),
						Precision = ptr->Precision,
						Scale = ptr->Scale
					};
					num2 += 1U;
				}
				foreach (DbParameter dbParameter in dictionary.Values)
				{
					this.parameters[dbParameter.Ordinal] = dbParameter;
					if (dbParameter.Name != null)
					{
						this.parameter2Ordinal[dbParameter.Name] = dbParameter.Ordinal;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006DD4 File Offset: 0x00004FD4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe int MapParameterNames(DB_UPARAMS paramNameCount, char** paramNames, DB_LPARAMS* paramOrdinals)
		{
			for (uint num = 0U; num < (uint)paramNameCount.Value; num += 1U)
			{
				string text = new string(*(IntPtr*)(paramNames + (ulong)num * (ulong)((long)sizeof(char*)) / (ulong)sizeof(char*)));
				paramOrdinals[(ulong)num * (ulong)((long)sizeof(DB_LPARAMS)) / (ulong)sizeof(DB_LPARAMS)].Value = (long)this.parameter2Ordinal[text];
			}
			return 0;
		}

		// Token: 0x0400008D RID: 141
		private readonly SortedDictionary<uint, DbParameter> parameters;

		// Token: 0x0400008E RID: 142
		private readonly Dictionary<string, uint> parameter2Ordinal;

		// Token: 0x0400008F RID: 143
		private readonly Dictionary<string, DBTYPE> typeTranslator;

		// Token: 0x04000090 RID: 144
		private readonly Dictionary<DBTYPE, string> typeReverseTranslator;
	}
}
