namespace Life.Server

type DataStoreOperation = DataStoreOperation of (AsyncReplyChannel<ServerState> *  (ServerState -> ServerState))

module DataStore =
    let create startState =
        MailboxProcessor.Start(fun agent -> 
            let rec loop state = async {
                let! DataStoreOperation (reply, f) = agent.Receive()
                let state' = f state
                reply.Reply state'
                return! loop state'
            }

            loop startState
        )

    let addPattern pattern (agent : MailboxProcessor<DataStoreOperation>) =
        let addPatternToState (s : ServerState) =
            { s with Patterns = pattern::s.Patterns }
        agent.PostAndAsyncReply(fun ch -> DataStoreOperation (ch, addPatternToState))

    let getState (agent : MailboxProcessor<DataStoreOperation>) =
        agent.PostAndAsyncReply(fun ch -> DataStoreOperation (ch, id))