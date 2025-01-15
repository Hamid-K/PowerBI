using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008E RID: 142
	[NullableContext(1)]
	[Nullable(0)]
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
	public abstract class HttpPipelineSynchronousPolicy : HttpPipelinePolicy
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000DF38 File Offset: 0x0000C138
		protected HttpPipelineSynchronousPolicy()
		{
			MethodInfo method = base.GetType().GetMethod("OnReceivedResponse", BindingFlags.Instance | BindingFlags.Public, null, HttpPipelineSynchronousPolicy._onReceivedResponseParameters, null);
			if (method != null)
			{
				this._hasOnReceivedResponse = method.GetBaseDefinition().DeclaringType != method.DeclaringType;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000DF91 File Offset: 0x0000C191
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.OnSendingRequest(message);
			HttpPipelinePolicy.ProcessNext(message, pipeline);
			this.OnReceivedResponse(message);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!this._hasOnReceivedResponse)
			{
				this.OnSendingRequest(message);
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			return this.InnerProcessAsync(message, pipeline);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		private async ValueTask InnerProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.OnSendingRequest(message);
			await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
			this.OnReceivedResponse(message);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E01F File Offset: 0x0000C21F
		public virtual void OnSendingRequest(HttpMessage message)
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E021 File Offset: 0x0000C221
		public virtual void OnReceivedResponse(HttpMessage message)
		{
		}

		// Token: 0x040001DB RID: 475
		private static Type[] _onReceivedResponseParameters = new Type[] { typeof(HttpMessage) };

		// Token: 0x040001DC RID: 476
		private readonly bool _hasOnReceivedResponse = true;
	}
}
