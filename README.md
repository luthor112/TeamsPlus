# TeamsPlus
Some tweaks for Teams

Currently implemented features:
- Support for both teams.live.com and teams.microsoft.com
  - Tweaks may apply to one or both
- Configuration through `%LOCALAPPDATA%\teamsplus\config.ini` (`Settings` button)
- CEF DevConsole (`Debug` button)
  - CEF DevConsole for second pane (Shift + `Debug` button)
- Always on Top toggle (`AOT` button)
- Split to two panels with a movable separator
  - `Clone` button: Clone the same session
  - Open a new session to log in with multiple accounts at the same time
    - `Secondary` button: Persist the session
    - `Temporary` button: Forget session data after closing
- UI tweaks
  - Change background colour or background image of the header
  - Change background colour or background image of the chat pane
- Links open in the default browser
- Make the first video feed fill the whole window (`Focus` and `Focus Secondary` buttons)
- Show/Hide the Chat List (`ChatList` and `ChatList Secondary` buttons)

Currently implemented configuration values:
- `theme.headerbg`, values: `#<HEX>, <URL>`
- `theme.headerbg-secondary`, values: `#<HEX>, <URL>`
- `theme.chatbg`, values: `#<HEX>, <URL>`
- `theme.chatblend`, values: float between 0.0 and 0.1
- `theme.chatbg-secondary`, values: `#<HEX>, <URL>`
- `theme.chatblend-secondary`, values: float between 0.0 and 0.1

TODO:
- Screenshot button
- Notification handling

Known bugs:
- `Focus` button sporadically doesn't work
  - Workaround: Click somewhere in Teams and try again within one second
  - This is a browser security feature