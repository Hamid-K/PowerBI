using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002EE RID: 750
	public class SSARegister : SSAValue
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0002E991 File Offset: 0x0002CB91
		private static string GenerateUniqueName()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("t_{0}", new object[] { SSARegister._nextUid += 1L }));
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0002E9BE File Offset: 0x0002CBBE
		public string Name { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0002E9C6 File Offset: 0x0002CBC6
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0002E9CE File Offset: 0x0002CBCE
		private string _desiredName { get; set; }

		// Token: 0x06001035 RID: 4149 RVA: 0x0002E9D7 File Offset: 0x0002CBD7
		public SSARegister(Type valueType)
			: this(SSARegister.GenerateUniqueName(), valueType, null)
		{
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		public SSARegister(string name = null, Type type = null, string desiredName = null)
			: base(type)
		{
			this.Name = name ?? SSARegister.GenerateUniqueName();
			this._desiredName = ((desiredName != null) ? FormattableString.Invariant(FormattableStringFactory.Create("{0}_{1}", new object[]
			{
				desiredName,
				SSARegister._nextUid
			})) : null);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002EA40 File Offset: 0x0002CC40
		public SSARegister CloneWithNewName(string newName)
		{
			SSARegister ssaregister = new SSARegister(newName, base.ValueType, this._desiredName);
			ssaregister.StepWhereDefined = base.StepWhereDefined;
			foreach (SSAValue ssavalue in base.ImmediateUpLinks)
			{
				ssaregister.ImmediateUpLinks.Add(ssavalue);
			}
			foreach (SSAValue ssavalue2 in base.ImmediateDownLinks)
			{
				ssaregister.ImmediateDownLinks.Add(ssavalue2);
			}
			return ssaregister;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002EB04 File Offset: 0x0002CD04
		public bool Equals(SSARegister other)
		{
			return other == this || (other != null && other.Name == this.Name);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002EB22 File Offset: 0x0002CD22
		public override bool Equals(SSAValue other)
		{
			return other != null && (other == this || this.Equals(other as SSARegister));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002EB3B File Offset: 0x0002CD3B
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() * 19423;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002EB4E File Offset: 0x0002CD4E
		public string GetName()
		{
			return this._desiredName ?? this.Name;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002EB60 File Offset: 0x0002CD60
		internal void SetDesiredName(string name)
		{
			this._desiredName = name;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002EB69 File Offset: 0x0002CD69
		public bool HasAGivenName()
		{
			return this._desiredName != null;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002EB4E File Offset: 0x0002CD4E
		public override string ToString()
		{
			return this._desiredName ?? this.Name;
		}

		// Token: 0x040007DA RID: 2010
		private static long _nextUid;
	}
}
