using System;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200014E RID: 334
	internal class InterceptorElement : ConfigurationElement
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x000384C7 File Offset: 0x000366C7
		public InterceptorElement(int key)
		{
			this.Key = key;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000384D6 File Offset: 0x000366D6
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000384DE File Offset: 0x000366DE
		internal int Key { get; private set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000384E7 File Offset: 0x000366E7
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000384F9 File Offset: 0x000366F9
		[ConfigurationProperty("type", IsRequired = true)]
		public virtual string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00038507 File Offset: 0x00036707
		[ConfigurationProperty("parameters")]
		public virtual ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003851C File Offset: 0x0003671C
		public virtual IDbInterceptor CreateInterceptor()
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(Type.GetType(this.TypeName, true), this.Parameters.GetTypedParameterValues());
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Strings.InterceptorTypeNotFound(this.TypeName), ex);
			}
			IDbInterceptor dbInterceptor = obj as IDbInterceptor;
			if (dbInterceptor == null)
			{
				throw new InvalidOperationException(Strings.InterceptorTypeNotInterceptor(this.TypeName));
			}
			return dbInterceptor;
		}

		// Token: 0x040009E4 RID: 2532
		private const string TypeKey = "type";

		// Token: 0x040009E5 RID: 2533
		private const string ParametersKey = "parameters";
	}
}
