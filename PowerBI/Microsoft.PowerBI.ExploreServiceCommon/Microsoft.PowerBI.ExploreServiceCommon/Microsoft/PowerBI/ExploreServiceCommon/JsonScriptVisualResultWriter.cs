using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OData.Json;
using Microsoft.ReportingServices.OData.Json.Extension;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x02000024 RID: 36
	public sealed class JsonScriptVisualResultWriter : IDisposable
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004D45 File Offset: 0x00002F45
		public JsonScriptVisualResultWriter(StreamWriter baseWriter)
		{
			this.m_baseWriter = baseWriter;
			this.m_writer = new JsonWriter(baseWriter, false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004D61 File Offset: 0x00002F61
		public void Dispose()
		{
			if (this.m_writer != null)
			{
				this.m_writer.Close();
				this.m_writer = null;
				this.m_baseWriter.Flush();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004D88 File Offset: 0x00002F88
		public void WriteResult(string objectId, Stream imageStream, string imageObjectId, IList<string> exceededDataLimitsIds)
		{
			using (this.m_writer.GetObjectScope())
			{
				using (this.m_writer.GetArrayPropertyScope("DataShapes"))
				{
					using (this.m_writer.GetObjectScope())
					{
						this.m_writer.WriteProperty("Id", objectId);
						using (this.m_writer.GetArrayPropertyScope("PrimaryHierarchy"))
						{
							using (this.m_writer.GetObjectScope())
							{
								this.m_writer.WriteProperty("Id", "DM0");
								using (this.m_writer.GetArrayPropertyScope("Instances"))
								{
									using (this.m_writer.GetObjectScope())
									{
										using (this.m_writer.GetArrayPropertyScope("Calculations"))
										{
											using (this.m_writer.GetObjectScope())
											{
												this.m_writer.WriteProperty("Id", imageObjectId);
												using (MemoryStream memoryStream = new MemoryStream())
												{
													imageStream.CopyTo(memoryStream);
													this.m_writer.WriteProperty("Value", memoryStream.ToArray());
												}
											}
										}
									}
								}
							}
						}
						if (exceededDataLimitsIds != null && exceededDataLimitsIds.Count > 0)
						{
							using (this.m_writer.GetArrayPropertyScope("DataLimitsExceeded"))
							{
								foreach (string text in exceededDataLimitsIds)
								{
									using (this.m_writer.GetObjectScope())
									{
										this.m_writer.WriteProperty("Id", text);
									}
								}
							}
						}
						this.m_writer.WriteProperty("IsComplete", true);
					}
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000050BC File Offset: 0x000032BC
		public void WriteException(Exception exception, string objectId, string errorCode = null, string message = null)
		{
			errorCode = errorCode ?? exception.GetType().FullName;
			message = message ?? exception.Message;
			using (this.m_writer.GetObjectScope())
			{
				using (this.m_writer.GetArrayPropertyScope("DataShapes"))
				{
					using (this.m_writer.GetObjectScope())
					{
						if (!string.IsNullOrEmpty(objectId))
						{
							this.m_writer.WriteProperty("Id", objectId);
						}
						using (this.m_writer.GetObjectPropertyScope("odata.error"))
						{
							this.m_writer.WriteProperty("code", errorCode);
							using (this.m_writer.GetObjectPropertyScope("message"))
							{
								this.m_writer.WriteProperty("lang", "en-US");
								this.m_writer.WriteProperty("value", message);
							}
							using (this.m_writer.GetArrayPropertyScope("azure:values"))
							{
								using (this.m_writer.GetObjectScope())
								{
									this.m_writer.WriteProperty("timestamp", DateTimeOffset.Now);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x040000DF RID: 223
		private JsonWriter m_writer;

		// Token: 0x040000E0 RID: 224
		private readonly StreamWriter m_baseWriter;
	}
}
