import { createRouter, createWebHistory } from 'vue-router'

import Home from '@/views/Home.vue'
import Table from '../views/Table.vue'

const routes = [
  { path: '/', component: Home },
  { path: '/table', component: Table },
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})
