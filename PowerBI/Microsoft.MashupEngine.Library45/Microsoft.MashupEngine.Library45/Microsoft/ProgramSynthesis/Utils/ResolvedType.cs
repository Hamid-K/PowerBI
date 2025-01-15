using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000503 RID: 1283
	[DataContract]
	public class ResolvedType : GrammarType, IEquatable<ResolvedType>
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00055847 File Offset: 0x00053A47
		public ResolvedType(Type type)
		{
			this._typeField = type;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00055856 File Offset: 0x00053A56
		public override bool? IsStatic
		{
			get
			{
				return new bool?(this.Type.IsStatic());
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00055868 File Offset: 0x00053A68
		public override Location Location
		{
			get
			{
				return new Location.Assembly(this.Type.GetTypeInfo().Assembly.Location, this.Type.Namespace, this.Type.Name, null);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0005589B File Offset: 0x00053A9B
		public override string Name
		{
			get
			{
				return this.Type.Name;
			}
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000558A8 File Offset: 0x00053AA8
		public override MethodInfo GetMethod(string name, BindingFlags bindingFlags, Type[] parameterTypes)
		{
			return this.Type.GetMethod(name, bindingFlags, parameterTypes);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000558B8 File Offset: 0x00053AB8
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			return this.Type.GetMethods(bindingFlags);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000558C6 File Offset: 0x00053AC6
		public override MemberInfo[] GetMember(string name)
		{
			return this.Type.GetMember(name);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000558D4 File Offset: 0x00053AD4
		public override string CsName()
		{
			return this.Type.CsName(true);
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x000558E2 File Offset: 0x00053AE2
		public override Type Type
		{
			get
			{
				return this._typeField;
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000558EA File Offset: 0x00053AEA
		public override string ToString()
		{
			return this.Type.ToString();
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x000558F7 File Offset: 0x00053AF7
		public bool Equals(ResolvedType other)
		{
			return this.Type == ((other != null) ? other.Type : null);
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00055910 File Offset: 0x00053B10
		public override bool Equals(object obj)
		{
			ResolvedType resolvedType = obj as ResolvedType;
			return resolvedType != null && this.Equals(resolvedType);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00055930 File Offset: 0x00053B30
		public override int GetHashCode()
		{
			return 6073 ^ this.Type.GetHashCode();
		}

		// Token: 0x04000DF5 RID: 3573
		[DataMember]
		private Type _typeField;
	}
}
