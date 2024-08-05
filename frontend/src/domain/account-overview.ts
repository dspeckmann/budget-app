import { Transaction } from './transaction'

export type AccountOverview = {
  account: string
  income: Transaction[],
  transfers: Transaction[],
  expenses: Transaction[],
}