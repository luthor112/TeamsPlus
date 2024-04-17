# TeamsPlus
Some tweaks for Teams

Currently implemented features:
- Support for both Teams Classic and New Teams
  - Tweaks may apply to one or both
- Configuration through `%LOCALAPPDATA%\teamsplus\config.ini`
- CEF DevConsole
- Always on Top toggle
- Split to two panels with a movable separator
  - Clone the same session
  - Open a new session to log in with multiple accounts at the same time
- UI tweaks
  - Remove "Help" and "Download desktop app" buttons
  - Change background colour or background image of the header
- Links open in the default browser

Currently implemented configuration values:
- `config.cleanup`, values: `true, false`, default: `true`
- `theme.headerbg`, values: `#<HEX>, <URL>`

Known bugs:
- UI tweaks sporadically don't get applied
  - Workaround: Change pages once