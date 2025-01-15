using System;
using System.Data;

namespace Dapper
{
	// Token: 0x02000007 RID: 7
	public sealed class DbString : SqlMapper.ICustomQueryParameter
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002428 File Offset: 0x00000628
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000242F File Offset: 0x0000062F
		public static bool IsAnsiDefault { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002437 File Offset: 0x00000637
		public DbString()
		{
			this.Length = -1;
			this.IsAnsi = DbString.IsAnsiDefault;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002451 File Offset: 0x00000651
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002459 File Offset: 0x00000659
		public bool IsAnsi { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002462 File Offset: 0x00000662
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000246A File Offset: 0x0000066A
		public bool IsFixedLength { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002473 File Offset: 0x00000673
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000247B File Offset: 0x0000067B
		public int Length { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002484 File Offset: 0x00000684
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000248C File Offset: 0x0000068C
		public string Value { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002498 File Offset: 0x00000698
		public void AddParameter(IDbCommand command, string name)
		{
			if (this.IsFixedLength && this.Length == -1)
			{
				throw new InvalidOperationException("If specifying IsFixedLength,  a Length must also be specified");
			}
			bool add = !command.Parameters.Contains(name);
			IDbDataParameter param;
			if (add)
			{
				param = command.CreateParameter();
				param.ParameterName = name;
			}
			else
			{
				param = (IDbDataParameter)command.Parameters[name];
			}
			param.Value = SqlMapper.SanitizeParameterValue(this.Value);
			if (this.Length == -1 && this.Value != null && this.Value.Length <= 4000)
			{
				param.Size = 4000;
			}
			else
			{
				param.Size = this.Length;
			}
			param.DbType = (this.IsAnsi ? (this.IsFixedLength ? DbType.AnsiStringFixedLength : DbType.AnsiString) : (this.IsFixedLength ? DbType.StringFixedLength : DbType.String));
			if (add)
			{
				command.Parameters.Add(param);
			}
		}

		// Token: 0x0400001A RID: 26
		public const int DefaultLength = 4000;
	}
}
