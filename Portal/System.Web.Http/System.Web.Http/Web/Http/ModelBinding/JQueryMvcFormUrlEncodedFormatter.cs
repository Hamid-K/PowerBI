using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000055 RID: 85
	public class JQueryMvcFormUrlEncodedFormatter : FormUrlEncodedMediaTypeFormatter
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00007177 File Offset: 0x00005377
		public JQueryMvcFormUrlEncodedFormatter()
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000717F File Offset: 0x0000537F
		public JQueryMvcFormUrlEncodedFormatter(HttpConfiguration config)
		{
			this._configuration = config;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000718E File Offset: 0x0000538E
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000071A8 File Offset: 0x000053A8
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (readStream == null)
			{
				throw new ArgumentNullException("readStream");
			}
			if (base.CanReadType(type))
			{
				return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
			}
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000071F8 File Offset: 0x000053F8
		private async Task<object> ReadFromStreamAsyncCore(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			TaskAwaiter<object> taskAwaiter = base.ReadFromStreamAsync(typeof(FormDataCollection), readStream, content, formatterLogger).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<object> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<object>);
			}
			FormDataCollection formDataCollection = (FormDataCollection)taskAwaiter.GetResult();
			object obj;
			try
			{
				obj = formDataCollection.ReadAs(type, string.Empty, this.RequiredMemberSelector, formatterLogger, this._configuration);
			}
			catch (Exception ex)
			{
				if (formatterLogger == null)
				{
					throw;
				}
				formatterLogger.LogError(string.Empty, ex);
				obj = MediaTypeFormatter.GetDefaultValueForType(type);
			}
			return obj;
		}

		// Token: 0x04000083 RID: 131
		private readonly HttpConfiguration _configuration;
	}
}
