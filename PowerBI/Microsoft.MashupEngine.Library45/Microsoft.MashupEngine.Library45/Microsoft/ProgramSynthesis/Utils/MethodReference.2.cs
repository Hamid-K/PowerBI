using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004B8 RID: 1208
	public abstract class MethodReference
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x000517BB File Offset: 0x0004F9BB
		protected MethodReference(MethodInfo methodInfo, bool handleParams)
		{
			this._methodInfo = methodInfo;
			this.HandleParams = handleParams;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000517D1 File Offset: 0x0004F9D1
		public Type Type
		{
			get
			{
				MethodInfo methodInfo = this._methodInfo;
				if (methodInfo == null)
				{
					return null;
				}
				return methodInfo.DeclaringType;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000517E4 File Offset: 0x0004F9E4
		public string MethodName
		{
			get
			{
				MethodInfo methodInfo = this._methodInfo;
				if (methodInfo == null)
				{
					return null;
				}
				return methodInfo.Name;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000517F7 File Offset: 0x0004F9F7
		public IReadOnlyList<Type> ParameterTypes
		{
			get
			{
				MethodInfo methodInfo = this._methodInfo;
				if (methodInfo == null)
				{
					return null;
				}
				return (from p in methodInfo.GetParameters()
					select p.ParameterType).ToList<Type>();
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00051833 File Offset: 0x0004FA33
		public bool HandleParams { get; }

		// Token: 0x06001B13 RID: 6931 RVA: 0x0005183B File Offset: 0x0004FA3B
		private static MethodReference<TDelegate> Create<TDelegate>(MethodInfo method, TDelegate @delegate, bool handleParams)
		{
			return new MethodReference<TDelegate>(method, @delegate, handleParams);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00051845 File Offset: 0x0004FA45
		public static MethodReference<TDelegate> WithoutReference<TDelegate>(TDelegate @delegate)
		{
			return MethodReference.Create<TDelegate>(null, @delegate, false);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0005184F File Offset: 0x0004FA4F
		public static MethodReference<TDelegate> Create<TDelegate>(MethodInfo method, TDelegate @delegate)
		{
			return MethodReference.Create<TDelegate>(method, @delegate, false);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00051859 File Offset: 0x0004FA59
		public static MethodReference<TDelegate> Create<TDelegate>(MethodInfo method, bool instance = false)
		{
			return MethodReference.Create<TDelegate>(method, method.ToDelegate(instance), false);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00051869 File Offset: 0x0004FA69
		public static MethodReference<TDelegate> CreateWithParams<TDelegate>(MethodInfo method, bool instance = false)
		{
			return MethodReference.Create<TDelegate>(method, method.ToDelegateWithParams(instance), true);
		}

		// Token: 0x04000D50 RID: 3408
		protected MethodInfo _methodInfo;
	}
}
