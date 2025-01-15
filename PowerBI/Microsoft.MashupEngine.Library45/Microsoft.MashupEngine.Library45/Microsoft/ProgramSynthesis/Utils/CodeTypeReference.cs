using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000409 RID: 1033
	public class CodeTypeReference
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x00047818 File Offset: 0x00045A18
		public CodeTypeReference(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				this.arrayRank = type.GetArrayRank();
				this.arrayElementType = new CodeTypeReference(type.GetElementType());
				this.baseType = null;
			}
			else
			{
				this.InitializeFromType(type);
				this.arrayRank = 0;
				this.arrayElementType = null;
			}
			this.isInterface = type.GetTypeInfo().IsInterface;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00047894 File Offset: 0x00045A94
		private void InitializeFromType(Type type)
		{
			this.baseType = type.Name;
			if (!type.IsGenericParameter)
			{
				Type type2 = type;
				while (type2.IsNested)
				{
					type2 = type2.DeclaringType;
					this.baseType = type2.Name + "+" + this.baseType;
				}
				if (!string.IsNullOrEmpty(type.Namespace))
				{
					this.baseType = type.Namespace + "." + this.baseType;
				}
			}
			if (type.GetTypeInfo().IsGenericType && !type.GetTypeInfo().ContainsGenericParameters)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					this.TypeArguments.Add(new CodeTypeReference(genericArguments[i]));
				}
				return;
			}
			if (!type.GetTypeInfo().IsGenericTypeDefinition)
			{
				this.needsFixup = true;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00047965 File Offset: 0x00045B65
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x0004796D File Offset: 0x00045B6D
		public CodeTypeReference ArrayElementType
		{
			get
			{
				return this.arrayElementType;
			}
			set
			{
				this.arrayElementType = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00047976 File Offset: 0x00045B76
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x0004797E File Offset: 0x00045B7E
		public int ArrayRank
		{
			get
			{
				return this.arrayRank;
			}
			set
			{
				this.arrayRank = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00047987 File Offset: 0x00045B87
		internal int NestedArrayDepth
		{
			get
			{
				if (this.arrayElementType == null)
				{
					return 0;
				}
				return 1 + this.arrayElementType.NestedArrayDepth;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x000479A0 File Offset: 0x00045BA0
		public string BaseType
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.BaseType;
				}
				if (string.IsNullOrEmpty(this.baseType))
				{
					return string.Empty;
				}
				string text = this.baseType;
				if (this.needsFixup && this.TypeArguments.Count > 0)
				{
					text = text + "`" + this.TypeArguments.Count.ToString(CultureInfo.InvariantCulture);
				}
				return text;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00047A1F File Offset: 0x00045C1F
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x00047A27 File Offset: 0x00045C27
		[ComVisible(false)]
		public CodeTypeReferenceOptions Options
		{
			get
			{
				return this.referenceOptions;
			}
			set
			{
				this.referenceOptions = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00047A30 File Offset: 0x00045C30
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.arrayRank > 0 && this.arrayElementType != null)
				{
					return this.arrayElementType.TypeArguments;
				}
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x04000B3D RID: 2877
		private string baseType;

		// Token: 0x04000B3E RID: 2878
		private bool isInterface;

		// Token: 0x04000B3F RID: 2879
		private int arrayRank;

		// Token: 0x04000B40 RID: 2880
		private CodeTypeReference arrayElementType;

		// Token: 0x04000B41 RID: 2881
		private CodeTypeReferenceCollection typeArguments;

		// Token: 0x04000B42 RID: 2882
		private CodeTypeReferenceOptions referenceOptions;

		// Token: 0x04000B43 RID: 2883
		private bool needsFixup;
	}
}
