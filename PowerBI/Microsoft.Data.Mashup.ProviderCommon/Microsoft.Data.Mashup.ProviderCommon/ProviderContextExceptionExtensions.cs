using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000017 RID: 23
	internal static class ProviderContextExceptionExtensions
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00005770 File Offset: 0x00003970
		public static void ThrowIfTranslatedException(this ProviderContext providerContext, Exception exception)
		{
			Exception ex = providerContext.TranslateException(exception);
			if (ex != exception)
			{
				throw ex;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000578C File Offset: 0x0000398C
		public static Exception TranslateException(this ProviderContext providerContext, Exception exception)
		{
			if (!providerContext.NeedTranslate(exception))
			{
				return exception;
			}
			ValueException2 valueException = exception as ValueException2;
			if (valueException != null)
			{
				return providerContext.CreateValueKindException(valueException).CopyExceptionDataFrom(exception);
			}
			InvalidResourceCredentialsException ex = exception as InvalidResourceCredentialsException;
			if (ex != null)
			{
				return ProviderContextExceptionExtensions.TranslateException(providerContext, ex, InvalidResourceCredentialsException.ReasonString, new Func<object, object, string>(ProviderErrorStrings.Evaluation_Challenge_Result_CredentialsInvalid)).CopyExceptionDataFrom(exception);
			}
			UnpermittedResourceAccessException ex2 = exception as UnpermittedResourceAccessException;
			if (ex2 != null)
			{
				if (ex2.ResourceOrigin == null)
				{
					return ProviderContextExceptionExtensions.TranslateException(providerContext, ex2, UnpermittedResourceAccessException.ReasonString, new Func<object, object, string>(ProviderErrorStrings.Evaluation_Challenge_Result_PermissionRequired)).CopyExceptionDataFrom(exception);
				}
				string text = ex2.UserMessage ?? ProviderErrorStrings.Evaluation_Challenge_Result_PermissionRequired_Redirect(ex2.Resource.Kind, ex2.ResourceOrigin.Path, ex2.Resource.Path);
				return ProviderContextExceptionExtensions.TranslateException(providerContext, ex2, UnpermittedResourceAccessException.ReasonString, text).CopyExceptionDataFrom(exception);
			}
			else
			{
				ResourceEncryptedConnectionException ex3 = exception as ResourceEncryptedConnectionException;
				if (ex3 != null)
				{
					return ProviderContextExceptionExtensions.TranslateException(providerContext, ex3, ResourceEncryptedConnectionException.ReasonString, new Func<object, object, string>(ProviderErrorStrings.Evaluation_Challenge_Result_EncryptedConnectionFailure)).CopyExceptionDataFrom(exception);
				}
				ResourceEncryptionPrincipalNameMismatch resourceEncryptionPrincipalNameMismatch = exception as ResourceEncryptionPrincipalNameMismatch;
				if (resourceEncryptionPrincipalNameMismatch != null)
				{
					return ProviderContextExceptionExtensions.TranslateException(providerContext, resourceEncryptionPrincipalNameMismatch, ResourceEncryptionPrincipalNameMismatch.ReasonString, (object kind, object path) => ProviderErrorStrings.Evaluation_Challenge_Result_PrincipalNameMismatch(path)).CopyExceptionDataFrom(exception);
				}
				ResourceAccessAuthorizationException ex4 = exception as ResourceAccessAuthorizationException;
				if (ex4 != null)
				{
					return ProviderContextExceptionExtensions.TranslateException(providerContext, ex4, ResourceAccessAuthorizationException.ReasonString, new Func<object, object, string>(ProviderErrorStrings.Evaluation_Challenge_Result_CredentialAccessDenied)).CopyExceptionDataFrom(exception);
				}
				ResourceAccessForbiddenException ex5 = exception as ResourceAccessForbiddenException;
				if (ex5 != null)
				{
					return ProviderContextExceptionExtensions.TranslateException(providerContext, ex5, ResourceAccessForbiddenException.ReasonString, new Func<object, object, string>(ProviderErrorStrings.Evaluation_Challenge_Result_CredentialAccessForbidden)).CopyExceptionDataFrom(exception);
				}
				FirewallRuleException2 firewallRuleException = exception as FirewallRuleException2;
				if (firewallRuleException != null)
				{
					return providerContext.CreatePrivacySettingKindException(ProviderErrorStrings.Resource_Firewall_Rule_Error, firewallRuleException.Resources, firewallRuleException).CopyExceptionDataFrom(exception);
				}
				FirewallFlowException2 firewallFlowException = exception as FirewallFlowException2;
				if (firewallFlowException != null)
				{
					return providerContext.CreatePrivacyEnforcementKindException(ProviderErrorStrings.ReasonMessageFormat(ProviderErrorStrings.Resource_Firewall_Flow_Error, firewallFlowException.Message), firewallFlowException.Resources, null).CopyExceptionDataFrom(exception);
				}
				FirewallException2 firewallException = exception as FirewallException2;
				if (firewallException != null)
				{
					return providerContext.CreatePrivacyKindException(ProviderErrorStrings.ReasonMessageFormat(ProviderErrorStrings.Resource_Firewall_Flow_Error, firewallException.Message), null).CopyExceptionDataFrom(exception);
				}
				QueryPermissionException ex6 = exception as QueryPermissionException;
				if (ex6 != null)
				{
					Dictionary<string, object> dictionary = null;
					if (ex6.ParameterCount > 0)
					{
						object obj = ex6.ParameterNames ?? ex6.ParameterCount;
						dictionary = new Dictionary<string, object> { { "Parameters", obj } };
					}
					string text2 = DataSourceProperties.FromQueryPermission(ex6.Type);
					return providerContext.CreatePermissionKindException(ProviderErrorStrings.MissingPermission(ex6.Resource.Kind, ex6.Resource.Path, text2), ex6.Resource, text2, ex6.Message, dictionary).CopyExceptionDataFrom(exception);
				}
				UnpermittedResourceActionException ex7 = exception as UnpermittedResourceActionException;
				if (ex7 != null)
				{
					return providerContext.CreateCredentialKindException(ProviderErrorStrings.Evaluation_Challenge_Result_Actions(ex7.Resource.Kind), UnpermittedResourceActionException.ReasonString, ex7.Resource).CopyExceptionDataFrom(exception);
				}
				if (exception is OperationCanceledException)
				{
					return providerContext.CreateCanceledException().CopyExceptionDataFrom(exception);
				}
				DeadlockException ex8 = exception as DeadlockException;
				if (ex8 != null)
				{
					return providerContext.CreateDeadlockException(ex8.Message).CopyExceptionDataFrom(exception);
				}
				HostingException ex9 = exception as HostingException;
				if (ex9 != null)
				{
					return providerContext.CreateHostingKindException(ex9.Message, ex9.Reason).CopyExceptionDataFrom(exception);
				}
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("MashupResource/TryCheckException", null, TraceEventType.Information, null))
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, exception))
					{
						return exception;
					}
				}
				ErrorException ex10 = exception.ToErrorException();
				if (ex10.IsExpected)
				{
					return providerContext.CreateMashupKindException(ex10.Message, ex10);
				}
				return providerContext.CreateInternalKindException(ProviderErrorStrings.ErrorDuringEvaluation, ex10);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005B38 File Offset: 0x00003D38
		private static Exception TranslateException(ProviderContext providerContext, ResourceSecurityException resourceSecurityException, string reason, Func<object, object, string> errorMessageCreator)
		{
			string text = resourceSecurityException.UserMessage ?? errorMessageCreator(resourceSecurityException.Resource.Kind, resourceSecurityException.Resource.Path);
			return ProviderContextExceptionExtensions.TranslateException(providerContext, resourceSecurityException, reason, text);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005B75 File Offset: 0x00003D75
		private static Exception TranslateException(ProviderContext providerContext, ResourceSecurityException resourceSecurityException, string reason, string errorMessage)
		{
			if (resourceSecurityException.DataSourceLocation != null)
			{
				return providerContext.CreateCredentialKindException(errorMessage, reason, resourceSecurityException.DataSourceLocation, resourceSecurityException.DataSourceLocationOrigin);
			}
			return providerContext.CreateCredentialKindException(errorMessage, reason, resourceSecurityException.Resource, (resourceSecurityException.ResourceOrigin == null) ? null : resourceSecurityException.ResourceOrigin);
		}

		// Token: 0x04000096 RID: 150
		private const string Parameters = "Parameters";
	}
}
