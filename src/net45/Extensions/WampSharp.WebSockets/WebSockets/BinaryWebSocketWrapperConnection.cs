﻿using System;
using System.Net.WebSockets;
using WampSharp.Core.Message;
using WampSharp.V2.Authentication;
using WampSharp.V2.Binding;

namespace WampSharp.WebSockets
{
    public class BinaryWebSocketWrapperConnection<TMessage> : WebSocketWrapperConnection<TMessage>
    {
        private readonly IWampBinaryBinding<TMessage> mBinding;

        public BinaryWebSocketWrapperConnection(IWebSocketWrapper webSocket, IWampBinaryBinding<TMessage> binding, ICookieProvider cookieProvider, ICookieAuthenticatorFactory cookieAuthenticatorFactory) : 
            base(webSocket, binding, cookieProvider, cookieAuthenticatorFactory)
        {
            mBinding = binding;
        }

        protected BinaryWebSocketWrapperConnection(IClientWebSocketWrapper clientWebSocket, Uri addressUri, IWampBinaryBinding<TMessage> binding) :
            base(clientWebSocket, addressUri, binding.Name, binding)
        {
            mBinding = binding;
        }

        protected override ArraySegment<byte> GetMessageInBytes(WampMessage<object> message)
        {
            byte[] bytes = mBinding.Format(message);
            return new ArraySegment<byte>(bytes);
        }

        protected override WebSocketMessageType WebSocketMessageType
        {
            get
            {
                return WebSocketMessageType.Binary;
            }
        }
    }
}