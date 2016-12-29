// Copyright (c) 2014-2015 Wolfgang Borgsmüller
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// 1. Redistributions of source code must retain the above copyright 
//    notice, this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its 
//    contributors may be used to endorse or promote products derived 
//    from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS 
// OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

// Generated file. Do not edit.


using System;

namespace Chromium.Remote {
    using Event;

    /// <summary>
    /// Implement this structure to receive string values asynchronously.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_string_visitor_capi.h">cef/include/capi/cef_string_visitor_capi.h</see>.
    /// </remarks>
    public class CfrStringVisitor : CfrClientBase {

        internal static CfrStringVisitor Wrap(RemotePtr remotePtr) {
            if(remotePtr == RemotePtr.Zero) return null;
            var weakCache = CfxRemoteCallContext.CurrentContext.connection.weakCache;
            lock(weakCache) {
                var cfrObj = (CfrStringVisitor)weakCache.Get(remotePtr.ptr);
                if(cfrObj == null) {
                    cfrObj = new CfrStringVisitor(remotePtr);
                    weakCache.Add(remotePtr.ptr, cfrObj);
                }
                return cfrObj;
            }
        }



        private CfrStringVisitor(RemotePtr remotePtr) : base(remotePtr) {}
        public CfrStringVisitor() : base(new CfxStringVisitorCtorWithGCHandleRemoteCall()) {
            RemotePtr.connection.weakCache.Add(RemotePtr.ptr, this);
        }

        /// <summary>
        /// Method that will be executed.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_string_visitor_capi.h">cef/include/capi/cef_string_visitor_capi.h</see>.
        /// </remarks>
        public event CfrStringVisitorVisitEventHandler Visit {
            add {
                if(m_Visit == null) {
                    var call = new CfxStringVisitorSetCallbackRemoteCall();
                    call.self = RemotePtr.ptr;
                    call.index = 0;
                    call.active = true;
                    call.RequestExecution(RemotePtr.connection);
                }
                m_Visit += value;
            }
            remove {
                m_Visit -= value;
                if(m_Visit == null) {
                    var call = new CfxStringVisitorSetCallbackRemoteCall();
                    call.self = RemotePtr.ptr;
                    call.index = 0;
                    call.active = false;
                    call.RequestExecution(RemotePtr.connection);
                }
            }
        }

        internal CfrStringVisitorVisitEventHandler m_Visit;


    }

    namespace Event {

        /// <summary>
        /// Method that will be executed.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_string_visitor_capi.h">cef/include/capi/cef_string_visitor_capi.h</see>.
        /// </remarks>
        public delegate void CfrStringVisitorVisitEventHandler(object sender, CfrStringVisitorVisitEventArgs e);

        /// <summary>
        /// Method that will be executed.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_string_visitor_capi.h">cef/include/capi/cef_string_visitor_capi.h</see>.
        /// </remarks>
        public class CfrStringVisitorVisitEventArgs : CfrEventArgs {

            private CfxStringVisitorVisitRemoteEventCall call;

            internal string m_string;
            internal bool m_string_fetched;

            internal CfrStringVisitorVisitEventArgs(CfxStringVisitorVisitRemoteEventCall call) { this.call = call; }

            /// <summary>
            /// Get the String parameter for the <see cref="CfrStringVisitor.Visit"/> render process callback.
            /// </summary>
            public string String {
                get {
                    CheckAccess();
                    if(!m_string_fetched) {
                        m_string = call.string_str == IntPtr.Zero ? null : (call.string_length == 0 ? String.Empty : CfrRuntime.Marshal.PtrToStringUni(new RemotePtr(call.string_str), call.string_length));
                        m_string_fetched = true;
                    }
                    return m_string;
                }
            }

            public override string ToString() {
                return String.Format("String={{{0}}}", String);
            }
        }

    }
}