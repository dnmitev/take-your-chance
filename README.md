# take-your-chance

## The problem
Build a console application in .NET Core which mimics the operations of the player
wallet that powers our gaming experience.
## Requirements
The wallet is the heart of the gaming experience and as such, it needs to provide
the following features:
- In the beginning, the player starts with a balance of $0 and after every
operation, their new balance is displayed
- Money deposit - the player must be able to deposit funds
- Money withdrawal - the player must be able to withdraw funds
- Placing bets & accepting wins - the player must be able to play a simple
game that simulates a real slot game (see game rules below)
- Game rules - our game of chance provides a simple betting experience with
the following rules:
    - The game accepts bets between $1 and $10
    - The game plays out as follows:
        -  50% of the bets lose
        - 40% of the bets win up to x2 the bet amount
        - 10% of the bets win between x2 and x10 the bet amount
    - After every round the player balance is calculated as follows:
**{new balance} = {old balance} - {bet amount} + {win amount}**
    - The game ends when the player decides to leave
# Important note:
All operations that require an amount must include the amount as a positive
number, regardless of the direction of the balance.