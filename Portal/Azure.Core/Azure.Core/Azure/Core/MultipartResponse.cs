using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000054 RID: 84
	public static class MultipartResponse
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00007EB7 File Offset: 0x000060B7
		internal static InvalidOperationException InvalidBatchContentType(string contentType)
		{
			return new InvalidOperationException("Expected " + HttpHeader.Names.ContentType + " to start with multipart/mixed; boundary= but received " + contentType);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007ED3 File Offset: 0x000060D3
		internal static InvalidOperationException InvalidHttpStatusLine(string statusLine)
		{
			return new InvalidOperationException("Expected an HTTP status line, not " + statusLine);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007EE5 File Offset: 0x000060E5
		internal static InvalidOperationException InvalidHttpHeaderLine(string headerLine)
		{
			return new InvalidOperationException("Expected an HTTP header line, not " + headerLine);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007EF7 File Offset: 0x000060F7
		public static Response[] Parse(Response response, bool expectCrLf, CancellationToken cancellationToken)
		{
			return MultipartResponse.ParseAsync(response, expectCrLf, false, cancellationToken).EnsureCompleted<Response[]>();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007F08 File Offset: 0x00006108
		public static async Task<Response[]> ParseAsync(Response response, bool expectCrLf, CancellationToken cancellationToken)
		{
			return await MultipartResponse.ParseAsync(response, expectCrLf, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007F5C File Offset: 0x0000615C
		internal static async Task<Response[]> ParseAsync(Response parentResponse, bool expectBoundariesWithCRLF, bool async, CancellationToken cancellationToken)
		{
			Stream contentStream = parentResponse.ContentStream;
			string contentType = parentResponse.Headers.ContentType;
			string text;
			if (!MultipartResponse.GetBoundary(contentType, out text))
			{
				throw MultipartResponse.InvalidBatchContentType(contentType);
			}
			Dictionary<int, Response> responses = new Dictionary<int, Response>();
			List<Response> responsesWithoutId = new List<Response>();
			MultipartReader reader = new MultipartReader(text, contentStream)
			{
				ExpectBoundariesWithCRLF = expectBoundariesWithCRLF
			};
			for (MultipartSection multipartSection = await reader.GetNextSectionAsync(async, cancellationToken).ConfigureAwait(false); multipartSection != null; multipartSection = await reader.GetNextSectionAsync(async, cancellationToken).ConfigureAwait(false))
			{
				bool flag = true;
				string[] array;
				string text2;
				if (multipartSection.Headers.TryGetValue(HttpHeader.Names.ContentType, out array) && array.Length == 1 && MultipartResponse.GetBoundary(array[0], out text2))
				{
					reader = new MultipartReader(text2, multipartSection.Body)
					{
						ExpectBoundariesWithCRLF = true
					};
				}
				else
				{
					string[] array2;
					int num;
					if (!multipartSection.Headers.TryGetValue("Content-ID", out array2) || array2.Length != 1 || !int.TryParse(array2[0], out num))
					{
						num = 0;
						flag = false;
					}
					MemoryResponse response = new MemoryResponse();
					response.RequestFailedDetailsParser = parentResponse.RequestFailedDetailsParser;
					response.Sanitizer = parentResponse.Sanitizer;
					if (flag)
					{
						responses[num] = response;
					}
					else
					{
						responsesWithoutId.Add(response);
					}
					MemoryStream responseContent;
					using (BufferedReadStream body = new BufferedReadStream(multipartSection.Body, 4096))
					{
						string text3 = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(false);
						string[] array3 = text3.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
						if (array3.Length != 3)
						{
							throw MultipartResponse.InvalidHttpStatusLine(text3);
						}
						response.SetStatus(int.Parse(array3[1], CultureInfo.InvariantCulture));
						response.SetReasonPhrase(array3[2]);
						text3 = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(false);
						while (!string.IsNullOrEmpty(text3))
						{
							int num2 = text3.IndexOf(':');
							if (num2 <= 0)
							{
								throw MultipartResponse.InvalidHttpHeaderLine(text3);
							}
							response.AddHeader(text3.Substring(0, num2), text3.Substring(num2 + 1, text3.Length - num2 - 1).Trim());
							text3 = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(false);
						}
						responseContent = new MemoryStream();
						if (async)
						{
							await body.CopyToAsync(responseContent, (int)body.Length, cancellationToken).ConfigureAwait(false);
						}
						else
						{
							body.CopyTo(responseContent);
						}
						responseContent.Seek(0L, SeekOrigin.Begin);
						response.ContentStream = responseContent;
					}
					response = null;
					BufferedReadStream body = null;
					responseContent = null;
				}
			}
			Response[] array4 = new Response[responses.Count + responsesWithoutId.Count];
			for (int i = 0; i < responses.Count; i++)
			{
				array4[i] = responses[i];
			}
			for (int j = responses.Count; j < array4.Length; j++)
			{
				array4[j] = responsesWithoutId[j - responses.Count];
			}
			return array4;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007FB8 File Offset: 0x000061B8
		internal static async Task<string> ReadLineAsync(this BufferedReadStream stream, bool async, CancellationToken cancellationToken)
		{
			string text;
			if (async)
			{
				text = await stream.ReadLineAsync(4096, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				text = stream.ReadLine(4096);
			}
			return text;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000800C File Offset: 0x0000620C
		internal static async Task<MultipartSection> GetNextSectionAsync(this MultipartReader reader, bool async, CancellationToken cancellationToken)
		{
			MultipartSection multipartSection;
			if (async)
			{
				multipartSection = await reader.ReadNextSectionAsync(cancellationToken).ConfigureAwait(false);
			}
			else
			{
				multipartSection = reader.ReadNextSectionAsync(cancellationToken).GetAwaiter().GetResult();
			}
			return multipartSection;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000805F File Offset: 0x0000625F
		private static bool GetBoundary(string contentType, out string batchBoundary)
		{
			if (contentType == null || !contentType.StartsWith("multipart/mixed; boundary=", StringComparison.Ordinal))
			{
				batchBoundary = null;
				return false;
			}
			batchBoundary = contentType.Substring("multipart/mixed; boundary=".Length);
			return true;
		}

		// Token: 0x04000122 RID: 290
		private const int KB = 1024;

		// Token: 0x04000123 RID: 291
		private const int ResponseLineSize = 4096;

		// Token: 0x04000124 RID: 292
		private const string MultipartContentTypePrefix = "multipart/mixed; boundary=";

		// Token: 0x04000125 RID: 293
		private const string ContentIdName = "Content-ID";
	}
}
