﻿//----------------------------------------------------------------------- 
// ETP DevKit, 1.2
//
// Copyright 2019 Energistics
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using Energistics.Etp.Common.Datatypes;

namespace Energistics.Etp.Common
{
    /// <summary>
    /// Provides common functionality for ETP endpoint capabilities.
    /// </summary>
    public class EtpEndpointCapabilities : EtpCapabilities, IEndpointCapabilities
    {
        /// <summary>
        /// Initializes a new <see cref="EtpEndpointCapabilities"/> instance.
        /// </summary>
        public EtpEndpointCapabilities()
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EtpEndpointCapabilities"/> instance.
        /// </summary>
        /// <param name="version">The ETP version the capabilities are for.</param>
        public EtpEndpointCapabilities(EtpVersion version)
            : base(version)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EtpEndpointCapabilities"/> instance.
        /// </summary>
        /// <param name="capabilities">The capabilities to initialize this from.</param>
        public EtpEndpointCapabilities(IReadOnlyCapabilities capabilities)
            : base(capabilities)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="EtpEndpointCapabilities"/> instance.
        /// </summary>
        /// <param name="version">The ETP version the capabilities are for.</param>
        /// <param name="capabilities">The capabilities to initialize this from.</param>
        public EtpEndpointCapabilities(EtpVersion version, IReadOnlyDataValueDictionary capabilities)
            : base(version, capabilities)
        {
        }

        /// <summary>
        /// The minimum time period in seconds that a store keeps the GrowingSatus for a growing object "active" after the last new part resulting in a change to the object's end index was added to the object.
        /// </summary>
        public long? ActiveTimeoutPeriod { get; set; }

        /// <summary>
        /// Details on how a client can authorize itself for connecting to a server.
        /// </summary>
        public string[] AuthorizationDetails { get; set; }

        /// <summary>
        /// The maximum time period in seconds--under normal operation on an uncongested session--for these conditions: 
        /// <list type="bullet">
        /// <item>after a change in an endpoint before that endpoint sends a change notification covering the change to any subscribed endpoint in any session.</item>
        /// <item>if the the change was the result of a message WITHOUT a positive response, it is the maximum time until the change is reflected in read operations in any session.</item>
        /// <item>If the change was the result of a message WITH a positive response, it is the maximum time until the change is reflectedin sessions other than the session where the change was made.RECOMMENDATION: Set as short as possible (i.e.a few seconds).</item>
        /// </list>
        /// </summary>
        public long? ChangePropagationPeriod { get; set; }

        /// <summary>
        /// The minimum time period in seconds time that a store retains the Canonical URI of a deleted data object and any change annotations for channels and growing objects. 
        /// </summary>
        public long? ChangeRetentionPeriod { get; set; }

        /// <summary>
        /// The maximum count of multipart messages allowed in parallel, in a single protocol, from one endpoint to another. The limit applies separately to each protocol, and separately from client to server and from server to client. The limit for an endpoint applies to the multipart messages that endpoint can receive.
        /// </summary>
        public long? MaxConcurrentMultipart { get; set; }

        /// <summary>
        /// The maximum size in bytes of a data object allowed in a complete multipart message. Size in bytes is the size in bytes of the uncompressed string representation of the data object in the format in which it is sent or received.
        /// </summary>
        public long? MaxDataObjectSize { get; set; }

        /// <summary>
        /// The maximum size in bytes of each data object part allowed in a standalone message or a complete multipart message. Size in bytes is the total size in bytes of the uncompressed string representation of the data object part in the format in which it is sent or received.
        /// </summary>
        public long? MaxPartSize { get; set; }

        /// <summary>
        /// The maximum count of concurrent ETP sessions that may be established for a given endpoint, by a specific client.
        /// </summary>
        public long? MaxSessionClientCount { get; set; }

        /// <summary>
        /// The maximum count of concurrent ETP sessions that may be established for a given endpoint across all clients. The determination of whether this limit is exceeded should be made at the time of receiving the HTTP WebSocket upgrade or connect request.
        /// </summary>
        public long? MaxSessionGlobalCount { get; set; }

        /// <summary>
        /// The maximum size in bytes allowed for a single WebSocket frame. The limit to use during a session is the minimum of the client's and the server's endpoint capability, which should be determined by the limits imposed by the WebSocket library used by each endpoint.
        /// </summary>
        public long? MaxWebSocketFramePayloadSize { get; set; }

        /// <summary>
        /// The maximum size in bytes allowed for a complete WebSocket message, which is composed of one or more WebSocket frames. The limit to use during a session is the minimum of the client's and the server's endpoint capability, which should be determined by the limits imposed by the WebSocket library used by each endpoint.
        /// </summary>
        public long? MaxWebSocketMessagePayloadSize { get; set; }

        /// <summary>
        /// The maximum time period in seconds--under normal operation on an uncongested session--allowed between subsequent messages in the SAME multipart request or response. The period is measured as the time between when each message has been fully sent or received via the WebSocket.
        /// </summary>
        public long? MultipartMessageTimeoutPeriod { get; set; }

        /// <summary>
        /// The maximum time period in seconds allowed between a request and the standalone response message or the first message in the multipart response message. The period is measured as the time between when the request message has been successfully sent via the WebSocket and when the first or only response message has been fully received via the WebSocket. When calculating this period, any Acknowledge messages or empty placeholder responses are ignored EXCEPT where these are the only and final response(s) to the request.
        /// </summary>
        public long? ResponseTimeoutPeriod { get; set; }

        /// <summary>
        /// The maximum time period in seconds a server will wait to receive a RequestSession message from a client after the WebSocket connection has been established.
        /// </summary>
        public long? RequestSessionTimeoutPeriod { get; set; }

        /// <summary>
        /// The maximum time period in seconds a client or server will wait for a valid ETP session to be established.
        /// </summary>
        public long? SessionEstablishmentTimeoutPeriod { get; set; }

        /// <summary>
        /// Indicates whether an endpoint supports alternate URI formats--beyond the canonical Energistics URIs, which MUST be supported--for requests.
        /// </summary>
        public bool? SupportsAlternateRequestUris { get; set; }

        /// <summary>
        /// Indicates whether an endpoint supports message header extensions.
        /// </summary>
        public bool? SupportsMessageHeaderExtension { get; set; }
    }
}
