using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000400 RID: 1024
	internal class VarVec : IEnumerable<Var>, IEnumerable
	{
		// Token: 0x06002F89 RID: 12169 RVA: 0x00095FCA File Offset: 0x000941CA
		internal void Clear()
		{
			this.m_bitVector.Length = 0;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00095FD8 File Offset: 0x000941D8
		internal void And(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.And(other.m_bitVector);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x00095FF3 File Offset: 0x000941F3
		internal void Or(VarVec other)
		{
			this.Align(other);
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x00096010 File Offset: 0x00094210
		internal void Minus(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.m_bitVector.Length = this.m_bitVector.Length;
			varVec.m_bitVector.Not();
			this.And(varVec);
			this.m_command.ReleaseVarVec(varVec);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00096060 File Offset: 0x00094260
		internal bool Overlaps(VarVec other)
		{
			VarVec varVec = this.m_command.CreateVarVec(other);
			varVec.And(this);
			bool flag = !varVec.IsEmpty;
			this.m_command.ReleaseVarVec(varVec);
			return flag;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00096098 File Offset: 0x00094298
		internal bool Subsumes(VarVec other)
		{
			for (int i = 0; i < other.m_bitVector.Length; i++)
			{
				if (other.m_bitVector[i] && (i >= this.m_bitVector.Length || !this.m_bitVector[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000960E8 File Offset: 0x000942E8
		internal void InitFrom(VarVec other)
		{
			this.Clear();
			this.m_bitVector.Length = other.m_bitVector.Length;
			this.m_bitVector.Or(other.m_bitVector);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00096118 File Offset: 0x00094318
		internal void InitFrom(IEnumerable<Var> other)
		{
			this.InitFrom(other, false);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00096124 File Offset: 0x00094324
		internal void InitFrom(IEnumerable<Var> other, bool ignoreParameters)
		{
			this.Clear();
			foreach (Var var in other)
			{
				if (!ignoreParameters || var.VarType != VarType.Parameter)
				{
					this.Set(var);
				}
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x00096180 File Offset: 0x00094380
		public IEnumerator<Var> GetEnumerator()
		{
			return this.m_command.GetVarVecEnumerator(this);
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x0009618E File Offset: 0x0009438E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x00096198 File Offset: 0x00094398
		internal int Count
		{
			get
			{
				int num = 0;
				foreach (Var var in this)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000961E0 File Offset: 0x000943E0
		internal bool IsSet(Var v)
		{
			this.Align(v.Id);
			return this.m_bitVector.Get(v.Id);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000961FF File Offset: 0x000943FF
		internal void Set(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, true);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x0009621F File Offset: 0x0009441F
		internal void Clear(Var v)
		{
			this.Align(v.Id);
			this.m_bitVector.Set(v.Id, false);
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x0009623F File Offset: 0x0009443F
		internal bool IsEmpty
		{
			get
			{
				return this.First == null;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x0009624C File Offset: 0x0009444C
		internal Var First
		{
			get
			{
				using (IEnumerator<Var> enumerator = this.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
				return null;
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x00096294 File Offset: 0x00094494
		internal VarVec Remap(Dictionary<Var, Var> varMap)
		{
			VarVec varVec = this.m_command.CreateVarVec();
			foreach (Var var in this)
			{
				Var var2;
				if (!varMap.TryGetValue(var, out var2))
				{
					var2 = var;
				}
				varVec.Set(var2);
			}
			return varVec;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000962F8 File Offset: 0x000944F8
		internal VarVec(Command command)
		{
			this.m_bitVector = new BitArray(64);
			this.m_command = command;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x00096314 File Offset: 0x00094514
		private void Align(VarVec other)
		{
			if (other.m_bitVector.Length == this.m_bitVector.Length)
			{
				return;
			}
			if (other.m_bitVector.Length > this.m_bitVector.Length)
			{
				this.m_bitVector.Length = other.m_bitVector.Length;
				return;
			}
			other.m_bitVector.Length = this.m_bitVector.Length;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0009637F File Offset: 0x0009457F
		private void Align(int idx)
		{
			if (idx >= this.m_bitVector.Length)
			{
				this.m_bitVector.Length = idx + 1;
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000963A0 File Offset: 0x000945A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in this)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[] { text, var.Id });
				text = ",";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00096424 File Offset: 0x00094624
		public VarVec Clone()
		{
			VarVec varVec = this.m_command.CreateVarVec();
			varVec.InitFrom(this);
			return varVec;
		}

		// Token: 0x0400100E RID: 4110
		private readonly BitArray m_bitVector;

		// Token: 0x0400100F RID: 4111
		private readonly Command m_command;

		// Token: 0x02000A0A RID: 2570
		internal class VarVecEnumerator : IEnumerator<Var>, IDisposable, IEnumerator
		{
			// Token: 0x060060A4 RID: 24740 RVA: 0x0014C447 File Offset: 0x0014A647
			internal VarVecEnumerator(VarVec vec)
			{
				this.Init(vec);
			}

			// Token: 0x060060A5 RID: 24741 RVA: 0x0014C456 File Offset: 0x0014A656
			internal void Init(VarVec vec)
			{
				this.m_position = -1;
				this.m_command = vec.m_command;
				this.m_bitArray = vec.m_bitVector;
			}

			// Token: 0x170010A0 RID: 4256
			// (get) Token: 0x060060A6 RID: 24742 RVA: 0x0014C477 File Offset: 0x0014A677
			public Var Current
			{
				get
				{
					if (this.m_position < 0 || this.m_position >= this.m_bitArray.Length)
					{
						return null;
					}
					return this.m_command.GetVar(this.m_position);
				}
			}

			// Token: 0x170010A1 RID: 4257
			// (get) Token: 0x060060A7 RID: 24743 RVA: 0x0014C4A8 File Offset: 0x0014A6A8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060060A8 RID: 24744 RVA: 0x0014C4B0 File Offset: 0x0014A6B0
			public bool MoveNext()
			{
				this.m_position++;
				while (this.m_position < this.m_bitArray.Length)
				{
					if (this.m_bitArray[this.m_position])
					{
						return true;
					}
					this.m_position++;
				}
				return false;
			}

			// Token: 0x060060A9 RID: 24745 RVA: 0x0014C504 File Offset: 0x0014A704
			public void Reset()
			{
				this.m_position = -1;
			}

			// Token: 0x060060AA RID: 24746 RVA: 0x0014C50D File Offset: 0x0014A70D
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this.m_bitArray = null;
				this.m_command.ReleaseVarVecEnumerator(this);
			}

			// Token: 0x04002914 RID: 10516
			private int m_position;

			// Token: 0x04002915 RID: 10517
			private Command m_command;

			// Token: 0x04002916 RID: 10518
			private BitArray m_bitArray;
		}
	}
}
